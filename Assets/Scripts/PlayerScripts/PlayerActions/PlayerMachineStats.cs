using BuildableObjects;
using TMPro;
using UnityEngine;

namespace PlayerScripts.PlayerActions
{
    public class PlayerMachineStats : MonoBehaviour
    {
        private MachineObject _machineObject;
        private GameObject _stats;
        
        [SerializeField] 
        private GameObject statsPanel;
        private void Update()
        {
            if (_machineObject != null)
            {
                UpdatePanelPos();
                UpdatePanelText();
            }
        }
        
        private void Start()
        {
            _stats = gameObject;
            _stats.SetActive(false);
        }

        public void EnablePanel(MachineObject machineObject)
        {
            _machineObject = machineObject;
            UpdatePanelPos();
            UpdatePanelText();
            _stats.SetActive(true);
        }

        public void UpdatePanelPos()
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.y -= 200;
            statsPanel.transform.position = mousePos;
        }

        public void UpdatePanelText()
        {
            _stats.GetComponentInChildren<TextMeshProUGUI>().text = "Total Stored: " + _machineObject.GetWaterStorage().GetCount() + "L / " + _machineObject.GetWaterStorage().GetMax() + "L\nTotal Value: $" + _machineObject.GetWaterStorage().GetTotalValue();
        }
        
        public void DisablePanel()
        {
            _machineObject = null;
            _stats.SetActive(false);
        }
    }
}