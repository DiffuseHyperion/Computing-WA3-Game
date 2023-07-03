using UnityEngine;
using UnityEngine.UI;

namespace PlayerScripts.PlayerUI
{
    public class PlayerUIBuildButton : MonoBehaviour
    {
        private readonly float _alphaThreshold = 1f;
        private void Start()
        {
            GetComponent<Image>().alphaHitTestMinimumThreshold = _alphaThreshold;
        }
    }
}