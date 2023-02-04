using Apollo11.Player;
using UnityEngine;

namespace Apollo11
{
    public class MainMenuGnomeController : MonoBehaviour
    {
        [SerializeField] private PlayerWeaponsInHand weapons;

        private void Start()
        {
            weapons.SelectWeapon(Enums.HandWeapon.Pickaxe);
        }
    }
}
