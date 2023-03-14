using UnityEngine;

namespace Apollo11
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
