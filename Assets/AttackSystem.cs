using Apollo11.Core;
using Apollo11.Roots;
using UnityEngine;

namespace Apollo11
{
    public class AttackSystem : MonoBehaviour
    {
        public void TryAttack(IDamagable target)
        {
            var type = target.GetWeapon();
            if (SystemsLocator.Inst.WeaponsCharges.TakeCharges(type, 1))
            {
                target.TakeDamage(1);
            }
        }
    }
}
