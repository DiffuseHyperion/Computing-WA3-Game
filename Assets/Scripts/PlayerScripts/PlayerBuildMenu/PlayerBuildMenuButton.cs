using System.Collections.Generic;
using BuildableObjects;
using PlayerScripts.PlayerActions;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace PlayerScripts.PlayerBuildMenu
{
    public class PlayerBuildMenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        private List<BuildableObject> buildableObjects = new();
        [SerializeField]
        private bool progressButton;
        
        private PlayerBuildMenu _menu;
        private BuildableObject _currentBuildableObject;
        private int _currentIndex;
        private Button _button;

        private void Start()
        {
            _menu = GetComponentInParent<PlayerBuildMenu>();
            _button = gameObject.GetComponent<Button>();
            _currentBuildableObject = buildableObjects[_currentIndex];
            
            _button.GetComponentInChildren<TextMeshProUGUI>().text = _currentBuildableObject.GetName();
        }

        public void OnClick()
        {
            GetComponentInParent<PlayerBuilding>().CreateObject(_currentBuildableObject, progressButton && GetComponentInParent<PlayerMechanics>().GetMechanicLevel() <= _currentIndex);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _menu.GetDescriptionUI().EnablePanel(_currentBuildableObject);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _menu.GetDescriptionUI().GetComponent<PlayerBuildMenuDescription>().DisablePanel();
        }

        public void RotateRight()
        {
            _currentIndex += 1;
            if (_currentIndex >= buildableObjects.Count)
            {
                _currentIndex = 0;
            }
            _currentBuildableObject = buildableObjects[_currentIndex];
            
            
            _button.GetComponentInChildren<TextMeshProUGUI>().text = _currentBuildableObject.GetName();
            _menu.GetDescriptionUI().UpdatePanel(_currentBuildableObject);
        }

        public void RotateLeft()
        {
            _currentIndex -= 1;
            if (_currentIndex <= -1)
            {
                _currentIndex = buildableObjects.Count - 1;
            }
            _currentBuildableObject = buildableObjects[_currentIndex];
            
            
            _button.GetComponentInChildren<TextMeshProUGUI>().text = _currentBuildableObject.GetName();
            _menu.GetDescriptionUI().UpdatePanel(_currentBuildableObject);
        }
    }
}