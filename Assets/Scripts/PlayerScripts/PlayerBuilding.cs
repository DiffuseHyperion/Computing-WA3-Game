using BuildableObjects;
using UnityEngine;

namespace PlayerScripts
{
    public class PlayerBuilding : MonoBehaviour
    {
        public GameObject buildingButton;
        public GameObject buildingText;
        public BuildableObjectTicker buildableObjectTicker;
        private GameObject _placementGameObject;
        private BuildableObject _placementBuildableObject;
        private Player _player;
        private bool _building;
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
                    _placementGameObject.GetComponent<Collider2D>().enabled = true;
                    _placementGameObject.GetComponent<SpriteRenderer>().material.color = new Color(1f, 1f, 1f, 1f);

                    // oop is fun and all until you need to do this kekw
                    ITickableObject tickableObject = _placementGameObject.GetComponent<ITickableObject>();
                    if (tickableObject != null)
                    {
                        buildableObjectTicker.AddTickableObject(tickableObject);
                    }
                    
                    _placementBuildableObject.SetBuilt(true);
                    _placementBuildableObject.SetOwner(_player);
                    buildingButton.SetActive(true);
                    buildingText.SetActive(false);
                    _player.GetComponent<PlayerLinking>().ResetCooldown();
                    _building = false;
                    return;
                }
            }
            else {
                _placementGameObject.GetComponent<SpriteRenderer>().material.color = new Color(1f, 0f, 0f, 0.3f);
            }
            
            if (Input.GetMouseButtonDown(1)) {
                _player.moneyText.SetMoney(_player.moneyText.GetMoney() + _placementBuildableObject.GetCost());
                
                Destroy(_placementGameObject);
                _placementGameObject = null;
                _placementBuildableObject = null;
                
                buildingButton.SetActive(true);
                buildingText.SetActive(false);
                _player.GetComponent<PlayerLinking>().ResetCooldown();
                _building = false;
                return;
            }

            if (_delay > 0)
            {
                _delay--;
            }
        }

        public void CreateObject(BuildableObject buildableObject)
        {
            if (_building)
            {
                return;
            }

            if (_player.moneyText.GetMoney() < buildableObject.GetCost())
            {
                // play sound lol
            }
            else
            {
                _player.moneyText.SetMoney(_player.moneyText.GetMoney() - buildableObject.GetCost());
                _player.buildMenu.TogglePanel();
                _placementGameObject = Instantiate(buildableObject.gameObject);
                _placementBuildableObject = _placementGameObject.GetComponent<BuildableObject>();
                _placementGameObject.GetComponent<Collider2D>().enabled = false;
                _placementGameObject.GetComponent<SpriteRenderer>().material.color = new Color(1f, 1f, 1f, 0.3f);
                
                _player.buildMenu.description.GetComponent<PlayerBuildMenuDescription>().DisablePanel();
                buildingButton.SetActive(false);
                buildingText.SetActive(true);
                
                _delay = 200;
                _building = true;
            }
        }
    }
}