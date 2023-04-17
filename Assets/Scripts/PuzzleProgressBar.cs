using System;
using DG.Tweening;
using UnityEngine;

namespace Apollo11
{
    public class PuzzleProgressBar : MonoBehaviour
    {
        [SerializeField] private float progressAnimationDuration = 0.5f;
        [SerializeField] private Animator animator;
        [SerializeField] private SpriteRenderer mainRenderer;
        [SerializeField] private SpriteRenderer positiveRenderer;
        [SerializeField] private SpriteRenderer negativeRenderer;

        private Tween _mainRendererColorTween;
        private Tween _negativeColorTween;
        private Tween _positiveColorTween;
        private float _currentVal;

        public void SetProgress(float percentage01)
        {
            if (Math.Abs(percentage01 - 1f) < 0.01f) percentage01 = 0.99f;

            _mainRendererColorTween.Kill();
            _mainRendererColorTween = DOTween.To(()=>_currentVal, SetVisual, percentage01, progressAnimationDuration);
        }

        private void SetVisual(float val)
        {
            _currentVal = val;
            animator.Play("ProgressBar", 0, val);
        }

        public void IndicatePositive()
        {
            _positiveColorTween?.Kill();
            
            var posCol = positiveRenderer.color;
            posCol.a = 0f;
            positiveRenderer.color = posCol;

            _positiveColorTween = positiveRenderer.DOFade(1, 0.5f)
                .SetEase(Ease.OutQuad);
        }
        
        public void IndicateNegative(bool delayAnimation = false)
        {
            _negativeColorTween?.Kill();
            
            var negCol = negativeRenderer.color;
            negCol.a = 0f;
            negativeRenderer.color = negCol;

            var seq = DOTween.Sequence()
                .AppendInterval(delayAnimation ? progressAnimationDuration : 0f)
                .Append(negativeRenderer.DOFade(1, 0.3f)
                    .SetLoops(2, LoopType.Yoyo)
                    .SetEase(Ease.OutQuint));
            
            _negativeColorTween = seq;

        }


    }
}
