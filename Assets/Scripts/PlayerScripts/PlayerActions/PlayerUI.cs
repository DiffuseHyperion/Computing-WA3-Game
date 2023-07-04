using System;
using System.Collections.Generic;
using PlayerScripts.PlayerUI;
using UnityEngine;

namespace PlayerScripts.PlayerActions
{
    public class PlayerUI : MonoBehaviour
    {
        [SerializeField] 
        private PlayerUIBarNames[] uiBarNames;
        [SerializeField] 
        private PlayerUIBar[] uiBar;
        [SerializeField]
        private PlayerUIBuildButton buildButton;
        
        private Dictionary<PlayerUIBarNames, PlayerUIBar> _uiBars = new();
        public delegate void OnUIInitialized();
        public OnUIInitialized OnUIInitializedEvent;

        private void Awake()
        {
            InitUIBars();
        }

        void Start()
        {
            OnUIInitializedEvent.Invoke();
        }

        public T GetBar<T>(PlayerUIBarNames name) where T : PlayerUIBar
        {
            return (T) _uiBars[name];
        }

        private void InitUIBars()
        {
            if (uiBarNames.Length != uiBar.Length)
            {
                throw new ArgumentException("UIBarNames and UIBar lengths do not match up!");
            }

            // scuffed way of initializing dict because unity doesnt support it lol
            for (int i = 0; i < uiBarNames.Length; i++)
            {
                _uiBars.Add(uiBarNames[i], uiBar[i]);
            }
        }

        public PlayerUIBar[] GetUIBars()
        {
            return uiBar;
        }
    }
}