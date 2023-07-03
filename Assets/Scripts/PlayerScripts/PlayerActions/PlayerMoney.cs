using MechanicScripts;
using PlayerScripts.PlayerUI;
using TMPro;
using UnityEngine;

namespace PlayerScripts.PlayerActions
{
    public class PlayerMoney : MonoBehaviour
    {
        private int _money;
        private TextMeshProUGUI _moneyText;
        public int startingMoney;

        void Start()
        {
            gameObject.GetComponent<Player>().uiMenu.OnUIInitializedEvent += OnUIMenuInitialized;
        }

        void OnUIMenuInitialized()
        {
            _moneyText = gameObject.GetComponent<Player>().uiMenu.GetBar<PlayerUIBar>(PlayerUIBarNames.MONEY).GetText();
            SetMoney(startingMoney);
        }

        public int GetMoney()
        {
            return _money;
        }

        public void SetMoney(int money)
        {
            _money = money;
            if (_moneyText == null)
            {
                Debug.Log("moneyText null");
            }
            _moneyText.text = "Money: " + _money;
        }
        
        public void IncrementMoney(int money)
        {
            _money += money;
            _moneyText.text = "Money: " + _money;
        }
    }
}