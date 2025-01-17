using System;
using Apollo11.Core;
using SimpleInputNamespace;
using UnityEngine;

namespace Apollo11.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float speed = 5f;
        [Space]
        [SerializeField] private Rigidbody2D rb2d;
        

        public bool LockMovement { get; set; }
        public bool SuperLockMovement { get; set; }

        public Vector2 Movement{ get; private set; }

        private Joystick _joystick;

        private void Start()
        {
            _joystick = SystemsLocator.Inst.GameCanvas.TouchControls.Joystick;
        }

        void Update()
        {
            if (SystemsLocator.Inst.InPause) return;

            if (LockMovement || SuperLockMovement)
            {
                Movement = Vector2.zero;
                return;
            }
            
            Vector2 rawInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            if (_joystick.Value != Vector2.zero)
            {
                rawInput = _joystick.Value;
            }
            Movement = Vector2.ClampMagnitude(rawInput, 1f);
        }


        private void FixedUpdate()
        {
            if (SystemsLocator.Inst.InPause) return;
            
            var direction = (Vector3)Movement;
            rb2d.velocity = (direction * (speed));
        }
    }
}
