using System.Collections.Generic;
using PlayerScripts.PlayerActions;
using UnityEngine;
using UnityEngine.Serialization;

namespace PlayerScripts.PlayerBuildMenu
{
    public class PlayerBuildMenu : MonoBehaviour
    {
        [SerializeField]
        private GameObject description;

        [SerializeField] private GameObject leftButton;
        [SerializeField] private GameObject rightButton;
        
        private Player _player;
        private GameObject _panel;
        private readonly List<PlayerBuildMenuButton> _buttons = new();
        private int _currentIndex = 0;

        private void Start()
        {
            _player = GetComponentInParent<Player>();
            _panel = gameObject;
            _panel.SetActive(false);
            foreach (var button in GetComponentsInChildren<PlayerBuildMenuButton>())
            {
                _buttons.Add(button);
            }
            UpdateLeftRightButtons();
        }

        public void TogglePanel()
        {
            _panel.SetActive(!_panel.activeSelf);
        }

        public void SwitchLeft()
        {
            _currentIndex -= 1;
            foreach (var button in GetComponentsInChildren<PlayerBuildMenuButton>())
            {
                button.RotateLeft();
            }
            UpdateLeftRightButtons();
        }

        public void SwitchRight()
        {
            _currentIndex += 1;
            foreach (var button in GetComponentsInChildren<PlayerBuildMenuButton>())
            {
                button.RotateRight();
            }
            UpdateLeftRightButtons();
        }
        
        public PlayerBuildMenuDescription GetDescriptionUI()
        {
            return description.GetComponent<PlayerBuildMenuDescription>();
        }

        public void UpdateLeftRightButtons()
        {
            int mechanicLevel = gameObject.GetComponentInParent<PlayerMechanics>().GetMechanicLevel();
            rightButton.SetActive(mechanicLevel > _currentIndex);
            leftButton.SetActive(_currentIndex != 0);
        }
    }
}