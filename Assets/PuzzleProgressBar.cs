using System;
using DG.Tweening;
using UnityEngine;

namespace Apollo11
{
    public class PuzzleProgressBar : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private SpriteRenderer mainRenderer;
        [SerializeField] private SpriteRenderer positiveRenderer;
        [SerializeField] private SpriteRenderer negativeRenderer;

        private Tween _tween;
        private float _currentVal;
        
        public void SetProgress(float percentage01)
        {
            if (Math.Abs(percentage01 - 1f) < 0.01f) percentage01 = 0.99f;
            
            Debug.Log($"set progress to {percentage01}");
            
            _tween.Kill();
            _tween = DOTween.To(()=>_currentVal, SetVisual, percentage01, 0.5f);
        }

        private void SetVisual(float val)
        {
            _currentVal = val;
            animator.Play("ProgressBar", 0, val);
        }

        public void IndicatePositive()
        {
            
        }
        
        public void IndicateNegative()
        {
            
        }
        
    }
}
