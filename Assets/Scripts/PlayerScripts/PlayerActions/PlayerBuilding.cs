using BuildableObjects;
using BuildableObjects.Nodes;
using MechanicScripts;
using UnityEngine;

namespace PlayerScripts.PlayerActions
{
    public class PlayerBuilding : MonoBehaviour
    {
        [SerializeField] 
        private GameObject buildingButton;
        [SerializeField] 
        private GameObject buildingText;
        [SerializeField] 
        private BuildableObjectTicker buildableObjectTicker;
        
        private GameObject _placementGameObject;
        private BuildableObject _placementBuildableObject;
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

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = -1;
            _placementGameObject.transform.position = mousePos;
            if (Input.GetKey(KeyCode.R))
            {
                _placementGameObject.transform.Rotate(Vector3.back, 0.25f);
            }
            
            
            if (_placementBuildableObject.CanBuild()) {
                _placementGameObject.GetComponent<SpriteRenderer>().material.color = new Color(1f, 1f, 1f, 0.3f);
                if (Input.GetMouseButtonDown(0) && _delay <= 0) {
                    ConfirmBuildObject();
                    return;
                }
            }
            else {
                _placementGameObject.GetComponent<SpriteRenderer>().material.color = new Color(1f, 0f, 0f, 0.3f);
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
            Destroy(_placementGameObject);
            _placementGameObject = null;
            _placementBuildableObject = null;
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
            ITickableObject tickableObject = _placementGameObject.GetComponent<ITickableObject>();
            if (tickableObject != null)
            {
                buildableObjectTicker.AddTickableObject(tickableObject);
            }
            IPoweredObject poweredObject = _placementGameObject.GetComponent<IPoweredObject>();
            if (poweredObject != null)
            {
                GlobalMechanicManager.GetGlobalMechanicManager().GetMechanic<ElectricityMechanic>(GlobalMechanicNames.ELECTRICITY).IncreasePowerConsumption(poweredObject.GetPowerConsumption());
            }
            
            // deduct cash
            _player.GetComponent<PlayerMoney>().SetMoney(_player.GetComponent<PlayerMoney>().GetMoney() - _placementBuildableObject.GetCost());
            
            // enable colliders
            _placementGameObject.GetComponent<Collider2D>().enabled = true;
            foreach (var linkPort in _placementGameObject.GetComponentsInChildren<LinkPort>())
            {
                linkPort.gameObject.GetComponent<Collider2D>().enabled = true;
            }
            
            // toggle menus
            buildingButton.SetActive(true);
            buildingText.SetActive(false);
            
            // change object states
            _placementGameObject.GetComponent<SpriteRenderer>().material.color = new Color(1f, 1f, 1f, 1f);
            _placementBuildableObject.SetBuilt(true);
            _placementBuildableObject.OnBuild();
            
            _player.GetComponent<PlayerSoundManager>().PlayPlacedBuildingSound();
            _player.GetComponent<PlayerLinking>().ResetCooldown();
            _building = false;
            _progressing = false;
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
            _placementGameObject = Instantiate(buildableObject.gameObject);
            _placementBuildableObject = _placementGameObject.GetComponent<BuildableObject>();
            
            // init values within fields
            _placementBuildableObject.SetOwner(_player);
            
            // disable colliders in object
            _placementGameObject.GetComponent<Collider2D>().enabled = false;
            _placementGameObject.GetComponent<SpriteRenderer>().material.color = new Color(1f, 1f, 1f, 0.3f);
            foreach (var linkPort in _placementGameObject.GetComponentsInChildren<LinkPort>())
            {
                linkPort.gameObject.GetComponent<Collider2D>().enabled = false;
            }

            // init variables for loop
            _delay = 0.1f;
            _building = true;
            _progressing = progressMechanic;
        }
    }
}