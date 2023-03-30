using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Apollo11
{
    public class WallSymbol : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer unactiveRenderer;
        [SerializeField] private SpriteRenderer activeRenderer;

        private Tween _tween;
        private void Start()
        {
            _tween = activeRenderer.DOFade(125, 0.7f).SetLoops(-1).SetEase(Ease.InBack, 1, 10);
        }
        public void OnSolved()
        {
            _tween.Kill();
            activeRenderer.color = Color.white;
        }
        public void SetSprites(Sprite unactive, Sprite active)
        {
            unactiveRenderer.sprite = unactive;
            activeRenderer.sprite = active;
        }
    }
}
