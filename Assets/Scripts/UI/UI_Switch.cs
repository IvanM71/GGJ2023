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

        public void Init(bool startValue)
        {
            _handleX = handleTransform.anchoredPosition.x;
            ForceSetValue(startValue);
            toggle.onValueChanged.AddListener(AtValueChanged);
        }

        private void ForceSetValue(bool isOn)
        {
            var vector = handleTransform.anchoredPosition;
            vector.x = isOn ? _handleX : _handleX * -1;
            handleTransform.anchoredPosition = vector;

            handle.color = isOn ? onColor : offColor;
            background.color = isOn ? onColor : offColor;
        }

        private void AtValueChanged(bool isOn)
        {
            handleTransform.DOAnchorPosX(isOn ? _handleX: _handleX * -1 , 0.4f);

            handle.DOColor(isOn ? onColor : offColor, 0.4f);
            background.DOColor(isOn ? onColor : offColor, 0.4f);
        }

        
    }
}
