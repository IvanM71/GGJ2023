using Apollo11.Core;
using Apollo11.Roots;
using UnityEngine;

namespace Apollo11
{
    public class AttackSystem : MonoBehaviour
    {
        private IDamagable _currentTarget;
        
        public void TryAttack(IDamagable target)
        {
            if (target == null) return;

            var type = target.GetWeapon();
            if (SystemsLocator.Inst.WeaponsCharges.TakeCharges(type, 1))
            {
                SystemsLocator.Inst.InteractionSystem.InAttack = true;
                SystemsLocator.Inst.PlayerSystems.PlayerMovement.LockMovement = true;
                SystemsLocator.Inst.PlayerSystems.PlayerAnimation.PlayAttack(Enums.RootWeaponToHandWeapon(type));
                _currentTarget = target;
                
                
            }
        }

        public void AtAttackAnimation()
        {
            if (_currentTarget == null) return;
                
            _currentTarget.TakeDamage(1);
            SystemsLocator.Inst.SoundController.PlayToolHit(Enums.RootWeaponToHandWeapon(_currentTarget.GetWeapon()));


        }
    }
}
