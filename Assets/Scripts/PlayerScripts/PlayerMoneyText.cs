using TMPro;
using UnityEngine;

namespace PlayerScripts
{
    public class PlayerMoneyText : MonoBehaviour
    {
        private TextMeshProUGUI _moneyText;
        private int _money;
        public int startingMoney;

        void Start()
        {
            _moneyText = gameObject.GetComponent<TextMeshProUGUI>();
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
    }
}