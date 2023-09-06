using BuildableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace PlayerScripts.PlayerBuildMenu
{
    public class PlayerBuildMenuButtonPreview : MonoBehaviour
    {
        private Image _renderer;

        private void Start()
        {
            _renderer = GetComponent<Image>();
        }

        public void SetPreview(BuildableObject buildableObject)
        {
            _renderer.sprite = buildableObject.GetComponent<SpriteRenderer>().sprite;
            _renderer.color = buildableObject.GetComponent<SpriteRenderer>().color;
        }
    }
}