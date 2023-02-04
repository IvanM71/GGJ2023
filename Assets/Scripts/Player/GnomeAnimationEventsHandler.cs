using Apollo11.Core;
using UnityEngine;

namespace Apollo11.Player
{
    public class GnomeAnimationEventsHandler : MonoBehaviour
    {
        public void AtAttackDone()
        {
            print("AtAttackDone");
            if (SystemsLocator.Inst.InteractionSystem.InAttack)
            {
                SystemsLocator.Inst.PlayerSystems.PlayerAnimation.StopHandWeapon();
                SystemsLocator.Inst.PlayerSystems.PlayerMovement.LockMovement = false;
                SystemsLocator.Inst.InteractionSystem.InAttack = false;
            }
           
            
        }

        public void AtDealDamage()
        {
            SystemsLocator.Inst.AttackSystem.AtAttackAnimation();
        }

        public void AtStep()
        {
            SystemsLocator.Inst.SoundController.PlayStep();
        }

        public void AtToolWhoop()
        {
            SystemsLocator.Inst.SoundController.PlayToolWoosh();
        }

        public void AtToolHit()
        {
            SystemsLocator.Inst.PlayerSystems.PlayerWeaponsInHand.PlayHitSoundForSelectedWeapon();
        }
    }
}
