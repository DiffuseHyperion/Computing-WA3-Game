﻿using BuildableObjects;
using TMPro;
using UnityEngine;

namespace PlayerScripts.PlayerBuildMenu
{
    public class PlayerBuildMenuDescription : MonoBehaviour
    {
        
        private GameObject _description;
        
        [SerializeField]
        private GameObject descriptionPanel;
        private void Update()
        {
            UpdatePanelPos();
        }
        private void Start()
        {
            _description = gameObject;
            _description.SetActive(false);
        }

        public void EnablePanel(BuildableObject buildableObject)
        {
            UpdatePanel(buildableObject);
            UpdatePanelPos();
            _description.SetActive(true);
        }

        public void UpdatePanel(BuildableObject buildableObject)
        {
            _description.GetComponentInChildren<TextMeshProUGUI>().text = buildableObject.GetDescription() + "\n\nCost: " + buildableObject.GetCost();
        }

        public void DisablePanel()
        {
            _description.SetActive(false);
        }

        private void UpdatePanelPos()
        {
            Vector3 mousePos = Input.mousePosition;
            descriptionPanel.transform.position = mousePos;
        }
        
    }
}