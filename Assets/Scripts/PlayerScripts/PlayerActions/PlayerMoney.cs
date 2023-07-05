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
        private Player _player;
        [SerializeField] 
        private int startingMoney;

        void Awake()
        {
            _player = gameObject.GetComponent<Player>();
            _player.GetUIMenu().OnUIInitializedEvent += OnUIMenuInitialized;
        }

        void OnUIMenuInitialized()
        {
            _moneyText = _player.GetUIMenu().GetBar<PlayerUIBar>(PlayerUIBarNames.MONEY).GetText();
            SetMoney(startingMoney);
        }

        public int GetMoney()
        {
            return _money;
        }

        public void SetMoney(int money)
        {
            _money = money;
            _moneyText.text = "Money: " + _money;
        }
        
        public void IncrementMoney(int money)
        {
            _money += money;
            _moneyText.text = "Money: " + _money;
        }

        public void FlashError()
        {
            FlashRed();
            Invoke(nameof(FlashWhite), 0.2f);
            Invoke(nameof(FlashRed), 0.4f);
            Invoke(nameof(FlashWhite), 0.6f);
            Invoke(nameof(FlashRed), 0.8f);
            Invoke(nameof(FlashWhite), 1f);
        }

        private void FlashRed()
        {
            _moneyText.color = new Color(1f, 0, 0);
        }
        
        private void FlashWhite()
        {
            _moneyText.color = new Color(1f, 1f, 1f);
        }
    }
}