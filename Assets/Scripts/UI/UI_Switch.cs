using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Apollo11.UI
{
    public class UI_Switch : MonoBehaviour
    {
        [SerializeField] private RawImage background;
        [SerializeField] private Image handle;
        [SerializeField] private RectTransform handleTransform;

        [SerializeField] private Color onColor;
        [SerializeField] private Color offColor;

        private float _handleX;
        private bool _isOn = true;

        public void Init()
        {
            _handleX = handleTransform.anchoredPosition.x;
        }
        
        public void OnClick()
        {
            _isOn = !_isOn;
            
            handleTransform.DOAnchorPosX(_isOn ? _handleX: _handleX * -1 , 0.4f);

            handle.DOColor(_isOn ? onColor : offColor, 0.4f);
            background.DOColor(_isOn ? onColor : offColor, 0.4f);
        }

        
    }
}
