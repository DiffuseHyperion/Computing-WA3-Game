using UnityEngine;

namespace PlayerScripts
{
    public class PlayerMovement : MonoBehaviour
    {
        public float speed;
        private Rigidbody2D _rigidbody;
        private Vector2 _move;
        
        void Start()
        {
            _rigidbody = gameObject.GetComponent<Rigidbody2D>();
        }
        
        void Update()
        {
            _move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }

        void FixedUpdate()
        {
            _rigidbody.velocity = _move * (speed * Time.deltaTime);
        }
    }
}