using UnityEngine;

namespace Apollo11.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float speed = 5f;
        [Space]
        [SerializeField] private Rigidbody2D rb2d;

        public bool LockMovement { get; set; }

        private Vector2 _movement;
        
        void Update()
        {
            if (LockMovement)
            {
                _movement = Vector2.zero;
                return;
            }
            
            Vector2 rawInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            _movement = Vector2.ClampMagnitude(rawInput, 1f);
        }


        private void FixedUpdate()
        {
            var direction = (Vector3)_movement;
            rb2d.velocity = (direction * (speed));
        }
    }
}
