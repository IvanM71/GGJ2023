using System;
using UnityEngine;

namespace Apollo11
{
    public class AttackIcon : MonoBehaviour
    {
        [SerializeField] private Sprite buttonSprite;
        [SerializeField] private Sprite axeIcon;
        [SerializeField] private Sprite sawIcon;
        [SerializeField] private Sprite sprayerIcon;
        [Space]
        [SerializeField] private SpriteRenderer buttonSpriteRenderer;
        [SerializeField] private SpriteRenderer weaponSpriteRenderer;

        public void ToggleShow(bool show)
        {
            gameObject.SetActive(show);
        }

        public void Place(Enums.RootWeapon weapon, Vector2 position)
        {
            transform.position = position;
            switch (weapon)
            {
                case Enums.RootWeapon.Axe:
                    buttonSpriteRenderer.sprite = axeIcon;
                    break;
                case Enums.RootWeapon.Saw:
                    buttonSpriteRenderer.sprite = sawIcon;
                    break;
                case Enums.RootWeapon.Sprayer:
                    buttonSpriteRenderer.sprite = sprayerIcon;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(weapon), weapon, null);
            }
        }
    }
}
