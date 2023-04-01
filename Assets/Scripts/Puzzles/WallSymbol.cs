using DG.Tweening;
using UnityEngine;

namespace Apollo11.Puzzles
{
    public class WallSymbol : MonoBehaviour, IBreathBlinkPuzzle
    {
        [SerializeField] private SpriteRenderer unactiveRenderer;
        [SerializeField] private SpriteRenderer activeRenderer;
        
        public void SetSprites(Sprite unactive, Sprite active)
        {
            unactiveRenderer.sprite = unactive;
            activeRenderer.sprite = active;
        }

        public void BreathBlink(float halfBreathTime, float intensity01)
        {
            activeRenderer.DOFade(intensity01, halfBreathTime).SetEase(Ease.OutQuad).SetLoops(2, LoopType.Yoyo);
        }
    }
}
