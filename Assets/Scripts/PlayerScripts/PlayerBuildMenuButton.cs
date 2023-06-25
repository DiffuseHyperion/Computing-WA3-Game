using System;
using BuildableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace PlayerScripts
{
    public class PlayerBuildMenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private PlayerBuildMenu _menu;
        public BuildableObject buildableObject;
        private Button _button;

        private void Start()
        {
            _menu = GetComponentInParent<PlayerBuildMenu>();
            _button = gameObject.GetComponent<Button>();
            _button.GetComponentInChildren<TextMeshProUGUI>().text = buildableObject.GetName();
        }

        public void OnClick()
        {
            GetComponentInParent<PlayerBuilding>().CreateObject(buildableObject);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _menu.description.GetComponent<PlayerBuildMenuDescription>().EnablePanel(buildableObject);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _menu.description.GetComponent<PlayerBuildMenuDescription>().DisablePanel();
        }
    }
}