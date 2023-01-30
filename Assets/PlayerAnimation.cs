using System;
using Apollo11.Player;
using UnityEngine;

namespace Apollo11
{
    public class PlayerAnimation : MonoBehaviour
    {
        [SerializeField] private Transform visualToMirror;
        [SerializeField] private PlayerMovement playerMovement;

        private readonly Vector3 moveLeftScale = new (1f, 1f, 1f);
        private readonly Vector3 moveRightScale = new (-1f, 1f, 1f);


        private void Update()
        {
            if (playerMovement.Movement != Vector2.zero)
            {
                PlayMove(playerMovement.Movement.x);
            }
        }

        public void PlayMove(float xMoveDir)
        {
            if (xMoveDir != 0f)
                visualToMirror.transform.localScale = xMoveDir < 0f ? moveLeftScale : moveRightScale;
            
        }
    }
}
