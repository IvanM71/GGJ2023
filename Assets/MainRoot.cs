using UnityEngine;
using UnityEngine.EventSystems;

namespace Apollo11
{
    public class MainRoot : MonoBehaviour, IDamagable, IPointerClickHandler
    {
        [SerializeField] private Enums.RootWeapon weaponToKill;
        public int Health { get; set; } = 7;

        public void TakeDamage()
        {
            Health--;
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
        }
    }

    public interface IDamagable
    {
        public int Health { get; set; }
        public void TakeDamage();
        public Enums.RootWeapon GetWeapon();
    }
}
