using UnityEngine;

namespace PlayerScripts.PlayerActions
{
    public class PlayerCamera : MonoBehaviour
    {
        private Camera _camera;
        private GameObject _player;


        private void Start()
        {
            _player = transform.parent.gameObject;
            _camera = gameObject.GetComponent<Camera>();
        }

        void Update()
        {
            if (Input.GetAxisRaw("Mouse ScrollWheel") > 0 && _camera.orthographicSize < 10) {
                _camera.orthographicSize += 1;
            }
            else if (Input.GetAxisRaw("Mouse ScrollWheel") < 0 && _camera.orthographicSize > 1) {
                _camera.orthographicSize -= 1;
            }
        }

        void LateUpdate()
        {
            Vector3 playerPos = _player.transform.position;
            playerPos.z -= 10;
            _camera.transform.position = playerPos;
            //_camera.transform.position = Vector3.Lerp(_camera.transform.position, playerPos, 10 * Time.deltaTime);
        }
    }
}