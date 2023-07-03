using TMPro;
using UnityEngine;

namespace PlayerScripts.PlayerUI
{
    public class PlayerUIBar : MonoBehaviour
    {
        public TextMeshProUGUI text;
        
        public void EnableBar()
        {
            // do animations later lul
            gameObject.SetActive(true);
        }
        
        public void DisableBar()
        {
            gameObject.SetActive(false);
        }

        public TextMeshProUGUI GetText()
        {
            return text;
        }
    }
}