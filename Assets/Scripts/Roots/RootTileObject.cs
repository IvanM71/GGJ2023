using System;
using Apollo11.Core;
using UnityEngine;

namespace Apollo11
{
    public class RootTileObject : MonoBehaviour, IDamagable
    {
        public int X { get; set; }
        public int Y { get; set; }
        

        public void TakeDamage(int damage)
        {
            SystemsLocator.Inst.SoundController.PlayRootsImpact();

            if (SystemsLocator.Inst.RootsSystem.RootsModel.roots[X, Y].Stage == Enums.RootStages.STAGE_1)
            {
                SystemsLocator.Inst.RootsSystem.RootsModel.roots[X, Y].Stage = Enums.RootStages.STAGE_0;
                SystemsLocator.Inst.RootsSystem.SpawnDrop(transform.position);
                Debug.Log("Root is dead");
            }
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

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent<PlayerHealth>(out var playerHealth))
                playerHealth.RootsPlayerTouches.Add(this);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent<PlayerHealth>(out var playerHealth))
                playerHealth.RootsPlayerTouches.Remove(this);
        }
    }
}
