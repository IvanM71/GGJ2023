using System;
using System.Collections.Generic;
using Apollo11.Core;
using UnityEngine;

namespace Apollo11
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private float damageInterval = 0.5f;
        [SerializeField] private float healInterval = 1f;
        [SerializeField] private float healDelay = 3f;

        [Space]
        [SerializeField] private PlayerDamageIndication damageIndication;
        
        [NonSerialized] public readonly List<RootTileObject> RootsPlayerTouches = new();

        public event Action OnPlayerDead;

        private int _health = 10;
        private DateTime _lastDamageTime;
        private DateTime _lastHealTime;

        private bool _isDead;

        private void Update()
        {
            if (_isDead) return;

            if (RootsPlayerTouches.Count != 0)
            {
                TryGetDamage();
            }

            TryHeal();
        }

        private void TryGetDamage()
        {
            if ((DateTime.Now - _lastDamageTime).TotalSeconds > damageInterval)
            {
                _lastDamageTime = DateTime.Now;
                _health--;
                damageIndication.Blink();
                if (_health <= 0) 
                {
                    _health = 0;
                    _isDead = true;
                    AtDeath();
                }
                SystemsLocator.Inst.UI_HealthPanel.SetHealth(_health);
                SystemsLocator.Inst.SoundController.PlayGetDamage();
            }
        }

        private void TryHeal()
        {
            if ((DateTime.Now - _lastDamageTime).TotalSeconds > healDelay && (DateTime.Now - _lastHealTime).TotalSeconds > healInterval)
            {
                _lastHealTime = DateTime.Now;
                _health++;
                if (_health > 10) _health = 10;
                
                SystemsLocator.Inst.UI_HealthPanel.SetHealth(_health);
            }
        }

        private void AtDeath()
        {
            SystemsLocator.Inst.SoundController.PlayThrowItem(); //TODO
            SystemsLocator.Inst.PlayerSystems.PlayerMovement.LockMovement = true;
            SystemsLocator.Inst.PlayerSystems.PlayerAnimation.PlayDeath();
            SystemsLocator.Inst.InteractionSystem.InAttack = true;
            OnPlayerDead?.Invoke();
        }
    }
}
