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
            statsPanel.transform.position = mousePos;
        }

        public void UpdatePanelText()
        {
            Debug.Log("for machine " + _machineObject.name + ", water storage count: " + _machineObject.GetWaterStorage().GetCount());
            _stats.GetComponentInChildren<TextMeshProUGUI>().text = "Total Stored: " + _machineObject.GetWaterStorage().GetCount() + "L / " + _machineObject.GetWaterStorage().GetMax() + "L\nTotal Value: $" + _machineObject.GetWaterStorage().GetTotalValue();
        }
        
        public void DisablePanel()
        {
            _machineObject = null;
            _stats.SetActive(false);
        }
    }
}