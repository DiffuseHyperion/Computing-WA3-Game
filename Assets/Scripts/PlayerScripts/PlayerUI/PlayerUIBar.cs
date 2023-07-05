using TMPro;
using UnityEngine;

namespace PlayerScripts.PlayerUI
{
    public class PlayerUIBar : MonoBehaviour
    {
        private TextMeshProUGUI _text;
        private Animator _animator;
        void Awake()
        {
            _animator = gameObject.GetComponent<Animator>();
            _text = GetComponentInChildren<TextMeshProUGUI>();
        }

        public void PreEnableBar() // skips animation
        {
            _animator.enabled = false;
            gameObject.SetActive(true);
        }

        public void EnableBar()
        {
            gameObject.SetActive(true);
        }
        
        public void DisableBar()
        {
            gameObject.SetActive(false);
        }

        public TextMeshProUGUI GetText()
        {
            return _text;
        }
    }
}