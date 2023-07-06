using System;
using System.Collections.Generic;
using Apollo11.Core;
using Apollo11.Roots;
using UnityEngine;

namespace Apollo11.Player
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
        
        public bool Invincible { get; set; }

        private int _health = 10;
        private double _lastDamageTime;
        private double _lastHealTime;

        private bool _isDead;

        private void Update()
        {
            if (_isDead || Invincible) return;

            if (RootsPlayerTouches.Count != 0)
            {
                TryGetDamage();
            }

            TryHeal();
        }

        private void TryGetDamage()
        {
            if (Time.time - _lastDamageTime > damageInterval)
            {
                _lastDamageTime = Time.time;
                _health--;
                damageIndication.Blink();
                if (_health <= 0) 
                {
                    _health = 0;
                    _isDead = true;
                    AtDeath();
                }
                SystemsLocator.Inst.GameCanvas.UI_HealthPanel.SetHealth(_health);
                SystemsLocator.Inst.SoundController.PlayGetDamage();
            }
        }

        private void TryHeal()
        {
            if (Time.timeAsDouble - _lastDamageTime > healDelay && Time.timeAsDouble - _lastHealTime > healInterval)
            {
                _lastHealTime = Time.timeAsDouble;
                _health++;
                if (_health > 10) _health = 10;
                
                SystemsLocator.Inst.GameCanvas.UI_HealthPanel.SetHealth(_health);
            }
        }

        private void AtDeath()
        {
            SystemsLocator.Inst.SoundController.PlayDeath();
            SystemsLocator.Inst.PlayerSystems.PlayerMovement.LockMovement = true;
            SystemsLocator.Inst.PlayerSystems.PlayerAnimation.PlayDeath();
            SystemsLocator.Inst.InteractionSystem.InAttack = true;
            OnPlayerDead?.Invoke();
        }
    }
}
