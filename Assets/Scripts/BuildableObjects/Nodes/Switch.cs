using UnityEngine;
using UnityEngine.UI;

namespace BuildableObjects.Nodes
{
    public class Switch : MonoBehaviour
    {
        private bool _state;
        private bool _isDown;
        private Outline _outline;
        private void OnMouseDown()
        {
            if (_isDown)
            {
                return;
            }
            _state = true;
            _isDown = true;
        }

        private void OnMouseUp()
        {
            _isDown = false;
        }

        public bool IsTurnedOn()
        {
            return _state;
        }
    }
}