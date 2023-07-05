using BuildableObjects;
using MechanicScripts;
using PlayerScripts.PlayerBuildMenu;
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
        private int _delay;

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
                _delay--;
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
            if (_progressing)
            {
                _player.GetComponent<PlayerMechanics>().IncreaseMechanicLevel();
                GlobalMechanicManager.GetGlobalMechanicManager()
                    .GetMechanic<Mechanic>(
                        (GlobalMechanicNames) _player.GetComponent<PlayerMechanics>().GetMechanicLevel()).EnableMechanic();
            }
            
            // oop is fun and all until you need to do this kekw
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
            
            _player.GetComponent<PlayerMoney>().SetMoney(_player.GetComponent<PlayerMoney>().GetMoney() - _placementBuildableObject.GetCost());
            _placementGameObject.GetComponent<Collider2D>().enabled = true;
            _placementGameObject.GetComponent<SpriteRenderer>().material.color = new Color(1f, 1f, 1f, 1f);
            _placementBuildableObject.SetBuilt(true);
            _placementBuildableObject.SetOwner(_player);
            buildingButton.SetActive(true);
            buildingText.SetActive(false);
            _player.GetComponent<PlayerLinking>().ResetCooldown();
            _building = false;
            _progressing = false;
        }

        public void CreateObject(BuildableObject buildableObject, bool progressMechanic)
        {
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
            _player.GetBuildMenu().TogglePanel();
            _placementGameObject = Instantiate(buildableObject.gameObject);
            _placementBuildableObject = _placementGameObject.GetComponent<BuildableObject>();
            _placementGameObject.GetComponent<Collider2D>().enabled = false;
            _placementGameObject.GetComponent<SpriteRenderer>().material.color = new Color(1f, 1f, 1f, 0.3f);
                
            _player.GetBuildMenu().GetDescriptionUI().DisablePanel();
            buildingButton.SetActive(false);
            buildingText.SetActive(true);
                
            _delay = 200;
            _building = true;
            _progressing = progressMechanic;
        }
    }
}