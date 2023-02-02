using Apollo11.Core;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Apollo11.Roots
{
    public class MainRoot : MonoBehaviour, IDamagable
    {
        [SerializeField] private Enums.RootWeapon weaponToKill;
        [SerializeField] private Enums.RootType rootType;
        public int Health { get; private set; } = 7;

        public void TakeDamage(int dmg)
        {
            print("Root takes damage!");
            Health -= dmg;
            if (Health<0) 
            {
                Destroy(gameObject); //TODO
            }
        }

        public Enums.RootWeapon GetWeapon() => weaponToKill;
        public Enums.RootType GetRootType() => rootType;
        public Vector2 GetPosition() => transform.position;
        public Vector2 GetIconPosition() => transform.position;

        /*public void OnPointerClick(PointerEventData eventData)
        {
            print("click");
            SystemsLocator.Inst.AttackSystem.TryAttack(this);
        }*/

        
    }
}
