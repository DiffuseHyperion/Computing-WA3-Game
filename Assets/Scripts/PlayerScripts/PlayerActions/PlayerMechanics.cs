using System;
using PlayerScripts.PlayerUI;
using UnityEngine;

namespace PlayerScripts.PlayerActions
{
    public class PlayerMechanics : MonoBehaviour
    {
        private int _mechanicLevel = 0;

        private void Start()
        {
            PlayerUI playerUI = GetComponent<Player>().uiMenu;
            playerUI.OnUIInitializedEvent += UpdateUIBars;
        }

        public int GetMechanicLevel()
        {
            return _mechanicLevel;
        }

        public void IncreaseMechanicLevel()
        {
            _mechanicLevel++;
            //UpdateUIBars();
        }
        
        public void UpdateUIBars()
        {
            PlayerUI playerUI = GetComponent<Player>().uiMenu;
            for (int i = 0; i < playerUI.UIBar.Length; i++)
            {
                PlayerUIBar bar = playerUI.UIBar[i];
                if (i <= _mechanicLevel)
                {
                    bar.EnableBar();
                }
                else
                {
                    bar.DisableBar();
                }
            }
        }
    }
}