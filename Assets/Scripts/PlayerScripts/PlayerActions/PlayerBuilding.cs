using BuildableObjects;
using BuildableObjects.Nodes;
using MechanicScripts;
using PlayerScripts.PlayerBuildMenu;
using UnityEngine;
using UtilClasses;

namespace PlayerScripts.PlayerActions
{
    public class PlayerBuilding : MonoBehaviour
    {
        [SerializeField] private GameObject buildingButton;
        [SerializeField] private GameObject buildingText;
        [SerializeField] private BuildableObjectTicker buildableObjectTicker;
        [SerializeField] private float buildDelay;
        [SerializeField] private TemporaryBuildableObject temporaryBuildableObjectPrefab;
        
        private TemporaryBuildableObject _temporaryBuildableObject;
        private BuildableObject _referenceBuildableObject;
        private Player _player;
        private bool _building;
        private bool _progressing;
        private float _delay;

        private void Start()
        {
            _player = gameObject.GetComponent<Player>();
            buildingButton.SetActive(true);
            buildingText.SetActive(false);
            _delay = 0;
        }

        
        void Update()
        {
            if (!_building)
            {
                return;
            }

            Vector3 mousePos = _player.gameObject.GetComponentInChildren<Camera>().ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = -1;
            _temporaryBuildableObject.transform.position = mousePos;
            if (Input.GetKey(KeyCode.R))
            {
                _temporaryBuildableObject.transform.Rotate(Vector3.back, 0.25f);
            }
            
            if (_referenceBuildableObject.GetBuildCondition().IsBuildable(_player)) {
                _temporaryBuildableObject.GetComponent<SpriteRenderer>().material.color = new Color(1f, 1f, 1f, 0.3f);
                if (Input.GetMouseButtonDown(0) && _delay <= 0) {
                    ConfirmBuildObject();
                    return;
                }
            }
            else {
                _temporaryBuildableObject.GetComponent<SpriteRenderer>().material.color = new Color(1f, 0f, 0f, 0.3f);
            }
            
            if (Input.GetMouseButtonDown(1)) {
                CancelBuildObject();
                return;
            }

            if (_delay > 0)
            {
                _delay -= Time.deltaTime;
            }
        }

        public void CancelBuildObject()
        {
            Destroy(_temporaryBuildableObject.gameObject);
            _temporaryBuildableObject = null;
            buildingButton.SetActive(true);
            buildingText.SetActive(false);
            _player.GetComponent<PlayerLinking>().ResetCooldown();
            _building = false;
        }

        public void ConfirmBuildObject()
        {
            // start progressing the player
            if (_progressing)
            {
                _player.GetComponent<PlayerMechanics>().IncreaseMechanicLevel();
                GlobalMechanicManager.GetGlobalMechanicManager()
                    .GetMechanic<Mechanic>(
                        (GlobalMechanicNames) _player.GetComponent<PlayerMechanics>().GetMechanicLevel()).EnableMechanic();
            }
            
            // update relevant components
            ITickableObject tickableObject = _referenceBuildableObject.GetComponent<ITickableObject>();
            if (tickableObject != null)
            {
                buildableObjectTicker.AddTickableObject(tickableObject);
            }
            IPoweredObject poweredObject = _referenceBuildableObject.GetComponent<IPoweredObject>();
            if (poweredObject != null)
            {
                GlobalMechanicManager.GetGlobalMechanicManager().GetMechanic<ElectricityMechanic>(GlobalMechanicNames.ELECTRICITY).IncreasePowerConsumption(poweredObject.GetPowerConsumption());
            }
            
            // deduct cash
            _player.GetComponent<PlayerMoney>().SetMoney(_player.GetComponent<PlayerMoney>().GetMoney() - _referenceBuildableObject.GetCost());
            
            // toggle menus
            buildingButton.SetActive(true);
            buildingText.SetActive(false);
            
            _player.GetComponent<PlayerLinking>().ResetCooldown();
            _building = false;
            _progressing = false;

            _temporaryBuildableObject.ConfirmBuild().OnBuild();
            Destroy(_temporaryBuildableObject.gameObject);
            
            
        }

        public void CreateObject(BuildableObject buildableObject, bool progressMechanic)
        {
            // check conditions
            if (_building)
            {
                return;
            }
            if (_player.GetComponent<PlayerMoney>().GetMoney() < buildableObject.GetCost())
            {
                _player.GetComponent<PlayerSoundManager>().PlayErrorSound();
                _player.GetComponent<PlayerMoney>().FlashError();
                return;
            }
            
            // toggle menus
            _player.GetBuildMenu().TogglePanel();
            _player.GetBuildMenu().GetDescriptionUI().DisablePanel();
            buildingButton.SetActive(false);
            buildingText.SetActive(true);
            
            // init fields
            _temporaryBuildableObject = Instantiate(temporaryBuildableObjectPrefab);
            _referenceBuildableObject = buildableObject;
            
            // init variables for loop
            _delay = buildDelay;
            _building = true;
            _progressing = progressMechanic;

            _temporaryBuildableObject.Init(_referenceBuildableObject, _player);
        }
    }
}