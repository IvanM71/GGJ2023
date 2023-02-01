using Apollo11.Core;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Apollo11.Roots
{
    public class MainRoot : MonoBehaviour, IDamagable, IPointerClickHandler
    {
        [SerializeField] private Enums.RootWeapon weaponToKill;
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

        public void OnPointerClick(PointerEventData eventData)
        {
            print("click");
            //TODO playerAttackSystem.TryAttack(this);
            SystemsLocator.Inst.AttackSystem.TryAttack(this);
        }
    }

    public interface IDamagable
    {
        public int Health { get;}
        public void TakeDamage(int dmg);
        public Enums.RootWeapon GetWeapon();
    }
}
