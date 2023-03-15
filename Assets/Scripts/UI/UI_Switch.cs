using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Apollo11.UI
{
    public class UI_Switch : MonoBehaviour
    {
        [SerializeField] private Toggle toggle;
        [SerializeField] private RawImage background;
        [SerializeField] private Image handle;
        [SerializeField] private RectTransform handleTransform;

        [SerializeField] private Color onColor;
        [SerializeField] private Color offColor;

        private float _handleX;
        
        Sequence _animationSequence;
        
        public void Init()
        {
            _handleX = handleTransform.anchoredPosition.x;
            ForceSetValue(toggle.isOn);
            toggle.onValueChanged.AddListener(AtValueChanged);
        }

        private void ForceSetValue(bool isOn)
        {
            _animationSequence?.Kill();
            
            var vector = handleTransform.anchoredPosition;
            vector.x = isOn ? _handleX : _handleX * -1;
            handleTransform.anchoredPosition = vector;

            handle.color = isOn ? onColor : offColor;
            background.color = isOn ? onColor : offColor;
        }

        private void AtValueChanged(bool isOn)
        {
            _animationSequence?.Kill();
            _animationSequence = DOTween.Sequence()
                .Append(handleTransform.DOAnchorPosX(isOn ? _handleX: _handleX * -1 , 0.4f))
                .Insert(0, handle.DOColor(isOn ? onColor : offColor, 0.4f))
                .Insert(0, background.DOColor(isOn ? onColor : offColor, 0.4f))
                .SetUpdate(UpdateType.Normal, true)
            ;
        }

        
    }
}
