using Apollo11.Core;
using Apollo11.Roots;
using UnityEngine;

namespace Apollo11
{
    public class AttackSystem : MonoBehaviour
    {
        public void TryAttack(IDamagable target)
        {
            if (target == null) return;

            var type = target.GetWeapon();
            if (SystemsLocator.Inst.WeaponsCharges.TakeCharges(type, 1))
            {
                SystemsLocator.Inst.InteractionSystem.InAttack = true;
                SystemsLocator.Inst.PlayerSystems.PlayerAnimation.PlayHandWeapon(Enums.RootWeaponToHandWeapon(type));
                target.TakeDamage(1);
            }
        }
    }
}
