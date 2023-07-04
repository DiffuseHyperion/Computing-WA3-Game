using PlayerScripts.PlayerUI;
using UnityEngine;

namespace PlayerScripts.PlayerActions
{
    public class PlayerMechanics : MonoBehaviour
    {
        private int _mechanicLevel;

        private void Awake()
        {
            PlayerUI playerUI = GetComponent<Player>().GetUIMenu();
            playerUI.OnUIInitializedEvent += UpdateUIBars;
        }

        public int GetMechanicLevel()
        {
            return _mechanicLevel;
        }

        public void IncreaseMechanicLevel()
        {
            _mechanicLevel++;
            GetComponent<Player>().GetBuildMenu().UpdateLeftRightButtons();
            // UpdateUIBars();    (Mechanic.EnableMechanic() already enables bar)
        }
        
        public void UpdateUIBars()
        {
            PlayerUI playerUI = GetComponent<Player>().GetUIMenu();
            for (int i = 0; i < playerUI.GetUIBars().Length; i++)
            {
                PlayerUIBar bar = playerUI.GetUIBars()[i];
                if (i <= _mechanicLevel)
                {
                    bar.PreEnableBar();
                }
                else
                {
                    bar.DisableBar();
                }
            }
        }
    }
}