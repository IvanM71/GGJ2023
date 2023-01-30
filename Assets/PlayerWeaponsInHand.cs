using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Apollo11
{
    public class PlayerWeaponsInHand : SerializedMonoBehaviour
    {
        [OdinSerialize] private Dictionary<Enums.HandWeapon, SpriteRenderer> handWeapons;

        private void Awake()
        {
            DeselectWeapon();
        }

        public void SelectWeapon(Enums.HandWeapon w)
        {
            if (handWeapons.ContainsKey(w) == false)
            {
                Debug.LogError($"No weapon {w} found!");
                return;
            }
            handWeapons[w].enabled = true;
        }

        public void DeselectWeapon()
        {
            foreach (var w in handWeapons)
            {
                w.Value.enabled = false;
            }
        }
    }
}
