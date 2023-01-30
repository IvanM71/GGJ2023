using System.Collections.Generic;
using UnityEngine;

namespace Apollo11
{
    public class PlayerWeaponsInHand : MonoBehaviour
    {
        [SerializeField] private List<Enums.HandWeapon> handWeaponsType;
        [SerializeField] private List<SpriteRenderer> handWeapons;

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
        }

        public void DeselectWeapon()
        {
            foreach (var w in handWeapons)
            {
                w.enabled = false;
            }
        }
    }
}
