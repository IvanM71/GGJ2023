using System.Collections.Generic;
using Apollo11.Core;
using UnityEngine;

namespace Apollo11.Player
{
    public class PlayerWeaponsInHand : MonoBehaviour
    {
        [SerializeField] private List<Enums.HandWeapon> handWeaponsType;
        [SerializeField] private List<SpriteRenderer> handWeapons;


        private Enums.HandWeapon _selectedHandWeapon;
        
        private void Awake()
        {
            DeselectWeapon();
        }

        public void SelectWeapon(Enums.HandWeapon w)
        {
            if (handWeaponsType.Contains(w) == false)
            {
                Debug.LogError($"No weapon {w} found!");
                return;
            }
            handWeapons[handWeaponsType.IndexOf(w)].enabled = true;
            _selectedHandWeapon = w;
        }

        public void DeselectWeapon()
        {
            foreach (var w in handWeapons)
            {
                w.enabled = false;
            }

            _selectedHandWeapon = Enums.HandWeapon.Unknown;
        }

        public void PlayHitSoundForSelectedWeapon()
        {
            SystemsLocator.Inst.SoundController.PlayToolHit(_selectedHandWeapon);
        }
    }
}
