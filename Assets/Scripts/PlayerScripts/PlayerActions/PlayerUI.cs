using System;
using System.Collections.Generic;
using PlayerScripts.PlayerUI;
using UnityEngine;

namespace PlayerScripts.PlayerActions
{
    public class PlayerUI : MonoBehaviour
    {
        private Dictionary<PlayerUIBarNames, PlayerUIBar> _uiBars = new();
        public PlayerUIBarNames[] UIBarNames;
        public PlayerUIBar[] UIBar;

        public PlayerUIBuildButton buildButton;
        
        public delegate void OnUIInitialized();
        public OnUIInitialized OnUIInitializedEvent;

        private void Start()
        {
            if (UIBarNames.Length != UIBar.Length)
            {
                throw new ArgumentException("UIBarNames and UIBar lengths do not match up!");
            }

            // scuffed way of initializing dict because unity doesnt support it lol
            for (int i = 0; i < UIBarNames.Length; i++)
            {
                _uiBars.Add(UIBarNames[i], UIBar[i]);
            }

            OnUIInitializedEvent();
        }

        public T GetBar<T>(PlayerUIBarNames name) where T : PlayerUIBar
        {
            return (T) _uiBars[name];
        }
    }
}