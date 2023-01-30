using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Apollo11
{
    public class PlayerWeaponsInHand : MonoBehaviour
    {
        [SerializeField] private Dictionary<Enums.HandWeapon, GameObject> handWeapons;
    }
}
