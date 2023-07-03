using System.Collections.Generic;
using UnityEngine;

namespace PlayerScripts.PlayerBuildMenu
{
    public class PlayerBuildMenu : MonoBehaviour
    {
        private GameObject _panel;
        public GameObject description;
        private readonly List<PlayerBuildMenuButton> _buttons = new();

        private void Start()
        {
            _panel = gameObject;
            _panel.SetActive(false);
            foreach (var button in GetComponentsInChildren<PlayerBuildMenuButton>())
            {
                _buttons.Add(button);
            }
        }

        public void TogglePanel()
        {
            bool isActive = _panel.activeSelf;
            _panel.SetActive(!isActive);
        }

        public void SwitchLeft()
        {
            foreach (var button in GetComponentsInChildren<PlayerBuildMenuButton>())
            {
                button.RotateLeft();
            }
        }

        public void SwitchRight()
        {
            foreach (var button in GetComponentsInChildren<PlayerBuildMenuButton>())
            {
                button.RotateRight();
            }
        }
    }
}