using System.Collections.Generic;
using UnityEngine;

namespace Apollo11.WeaponCharges
{
    public class WeaponsCharges : MonoBehaviour
    {
        [SerializeField] private Vector3Int startCharges;
        [Space]
        [SerializeField] private UI_WeaponsPanel uiPanel;

        private Dictionary<Enums.RootWeapon, int> data;

        private void Awake()
        {
            data = new Dictionary<Enums.RootWeapon, int>()
            {
                {Enums.RootWeapon.Axe, startCharges.x},
                {Enums.RootWeapon.Saw, startCharges.y},
                {Enums.RootWeapon.Sprayer, startCharges.z}
            };
            
            UpdateValues();
        }

        public void AddCharges(Enums.RootWeapon type, int amount)
        {
            data[type] += amount;
            UpdateValues();
        }

        public void TakeCharges(Enums.RootWeapon type, int amount)
        {
            var available = data[type];
            if (available - amount < 0)
            {
                //TODO indication
                return;
            }
            
            data[type] -= amount;
            UpdateValues();
        }

        private void UpdateValues()
        {
            uiPanel.SetValues(data[Enums.RootWeapon.Axe], data[Enums.RootWeapon.Saw], data[Enums.RootWeapon.Sprayer]);
        }
    }
}
