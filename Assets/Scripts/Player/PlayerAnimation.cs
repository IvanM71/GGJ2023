using UnityEngine;

namespace Apollo11.Player
{
    public class PlayerAnimation : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private Transform visualToMirror;
        [SerializeField] private PlayerMovement playerMovement;
        [SerializeField] private PlayerWeaponsInHand playerWeaponsInHand;
        [SerializeField] private PlayerItemCarry playerItemCarry;

        private readonly Vector3 moveLeftScale = new (1f, 1f, 1f);
        private readonly Vector3 moveRightScale = new (-1f, 1f, 1f);


        private void Update()
        {
            animator.SetBool("Carries", playerItemCarry.IsHoldingItem);
            
            if (playerMovement.Movement != Vector2.zero)
            {
                var xMoveDir = playerMovement.Movement.x;
                if (xMoveDir < 0f) visualToMirror.transform.localScale = moveLeftScale;
                else if (xMoveDir > 0f) visualToMirror.transform.localScale = moveRightScale;

                animator.SetBool("Walks", true);
            }
            else
            {
                animator.SetBool("Walks", false);
            }
        }


        public void PlayHandWeapon(Enums.HandWeapon weaponType)
        {
            playerWeaponsInHand.SelectWeapon(weaponType);
            animator.SetBool("Hits", true);
        }
        
        public void StopHandWeapon()
        {
            playerWeaponsInHand.DeselectWeapon();
            animator.SetBool("Hits", false);
            animator.SetBool("Attacks", false);
        }


        public void PlayAttack(Enums.HandWeapon weaponType)
        {
            animator.SetBool("Attacks", true);
            playerWeaponsInHand.SelectWeapon(weaponType);
        }

        public void PlayDeath()
        {
            animator.SetBool("Death", true);
        }
    }
}
