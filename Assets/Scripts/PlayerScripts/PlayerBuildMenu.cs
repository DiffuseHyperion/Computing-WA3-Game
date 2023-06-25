using UnityEngine;
using UnityEngine.Serialization;

namespace PlayerScripts
{
    public class PlayerBuildMenu : MonoBehaviour
    {
        private GameObject _panel;
        [FormerlySerializedAs("Description")] public GameObject description;

        private void Start()
        {
            _panel = gameObject;
            _panel.SetActive(false);
        }

        public void TogglePanel()
        {
            bool isActive = _panel.activeSelf;
            _panel.SetActive(!isActive);
        }
    }
}