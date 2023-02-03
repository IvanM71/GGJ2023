using System;
using Apollo11.Core;
using Apollo11.WorldUI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Apollo11.Roots
{
    public class MainRoot : MonoBehaviour, IDamagable
    {
        [SerializeField] private Enums.RootWeapon weaponToKill;
        [SerializeField] private Enums.RootType rootType;
        [SerializeField] private ProgressBar healthBar;
        public int Health { get; private set; } = 7;
        private int _startHealth;

        private void Awake()
        {
            _startHealth = Health;
        }

        public void TakeDamage(int dmg)
        {
            print("Root takes damage!");
            Health -= dmg;
            if (Health < 0) Health = 0;

            healthBar.SetValue01((float)Health / _startHealth);
            if (Health<=0) 
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
