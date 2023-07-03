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
        private PlayerBuildMenu _menu;
        public List<BuildableObject> buildableObjects = new();
        public bool progressButton;
        protected BuildableObject CurrentBuildableObject;
        private int _currentIndex;
        private Button _button;

        private void Start()
        {
            _menu = GetComponentInParent<PlayerBuildMenu>();
            _button = gameObject.GetComponent<Button>();
            CurrentBuildableObject = buildableObjects[_currentIndex];
            
            _button.GetComponentInChildren<TextMeshProUGUI>().text = CurrentBuildableObject.GetName();
        }

        public void OnClick()
        {
            GetComponentInParent<PlayerBuilding>().CreateObject(CurrentBuildableObject, progressButton && GetComponentInParent<PlayerMechanics>().GetMechanicLevel() <= _currentIndex);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _menu.description.GetComponent<PlayerBuildMenuDescription>().EnablePanel(CurrentBuildableObject);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _menu.description.GetComponent<PlayerBuildMenuDescription>().DisablePanel();
        }

        public void RotateRight()
        {
            _currentIndex += 1;
            if (_currentIndex >= buildableObjects.Count)
            {
                _currentIndex = 0;
            }
            CurrentBuildableObject = buildableObjects[_currentIndex];
            
            
            _button.GetComponentInChildren<TextMeshProUGUI>().text = CurrentBuildableObject.GetName();
            _menu.description.GetComponent<PlayerBuildMenuDescription>().UpdatePanel(CurrentBuildableObject);
        }

        public void RotateLeft()
        {
            _currentIndex -= 1;
            if (_currentIndex <= -1)
            {
                _currentIndex = buildableObjects.Count - 1;
            }
            CurrentBuildableObject = buildableObjects[_currentIndex];
            
            
            _button.GetComponentInChildren<TextMeshProUGUI>().text = CurrentBuildableObject.GetName();
            _menu.description.GetComponent<PlayerBuildMenuDescription>().UpdatePanel(CurrentBuildableObject);
        }
    }
}