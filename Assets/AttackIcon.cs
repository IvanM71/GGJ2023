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

        private void Awake()
        {
            buttonSpriteRenderer.sprite = buttonSprite;
        }

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
                    weaponSpriteRenderer.sprite = axeIcon;
                    break;
                case Enums.RootWeapon.Saw:
                    weaponSpriteRenderer.sprite = sawIcon;
                    break;
                case Enums.RootWeapon.Sprayer:
                    weaponSpriteRenderer.sprite = sprayerIcon;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(weapon), weapon, null);
            }
        }
    }
}
