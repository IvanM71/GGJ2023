using Apollo11.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Apollo11.Roots
{
    public class RootDamageReceiver : MonoBehaviour, IDamagable
    {
        public int X { get; set; }
        public int Y { get; set; }

        public void TakeDamage(int damage)
        {
            if ((int)SystemsLocator.Inst.RootsSystem.RootsModel.roots[X, Y].Stage < damage)
                SystemsLocator.Inst.RootsSystem.RootsModel.roots[X, Y].Stage = Enums.RootStages.STAGE_0;
            else
                SystemsLocator.Inst.RootsSystem.RootsModel.roots[X, Y].Stage -= damage;

            SystemsLocator.Inst.RootsSystem.UpdateView();
        }

        public Enums.RootWeapon GetWeapon()
        {
            switch (SystemsLocator.Inst.RootsSystem.RootsModel.roots[X, Y].Type)
            {
                case Enums.RootType.TypeA:
                    return Enums.RootWeapon.Axe;
                case Enums.RootType.TypeB:
                    return Enums.RootWeapon.Saw;
                case Enums.RootType.TypeC:
                    return Enums.RootWeapon.Sprayer;
                default:
                    return Enums.RootWeapon.Unknown;
            }
        }

        public Vector2 GetPosition()
        {
            return transform.position;
        }

        public Vector2 GetIconPosition()
        {
            return transform.position;
        }
    }
}
