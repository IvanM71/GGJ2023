using Apollo11.Core;
using UnityEngine;

namespace Apollo11.WorldUI
{
    public class InteractionControlsSpritesChanger : MonoBehaviour
    {
        [SerializeField] private Sprite AttackKeySprite;
        [SerializeField] private Sprite AttackIconSprite;
        [SerializeField] private Sprite UseKeySprite;
        [SerializeField] private Sprite UseIconSprite;
        [Space]
        [SerializeField] private SpriteRenderer attackButtonRenderer;
        [SerializeField] private SpriteRenderer useButtonRenderer;


        private void Awake()
        {
            ToggleTouchControls( PlayerSettings.Instance.TouchControls);
            PlayerSettings.Instance.OnTouchControlsValueChanged += ToggleTouchControls;
        }

        private void OnDestroy()
        {
            if (PlayerSettings.Instance!=null)
            {
                PlayerSettings.Instance.OnTouchControlsValueChanged -= ToggleTouchControls;
            }
            
        }

        private void ToggleTouchControls(bool touch)
        {
            if (touch)
            {
                attackButtonRenderer.sprite = AttackIconSprite;
                useButtonRenderer.sprite = UseIconSprite;
            }
            else
            {
                attackButtonRenderer.sprite = AttackKeySprite;
                useButtonRenderer.sprite = UseKeySprite;
            }
        }


    }
}
