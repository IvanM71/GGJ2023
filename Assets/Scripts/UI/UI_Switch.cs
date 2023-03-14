using System;
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

        private float handleX;

        public bool IsOn { get; private set; }
        public event Action<bool> OnValueChangedIsOn; 

        private void Awake()
        {
            handleX = handleTransform.anchoredPosition.x;
        }

        public void OnClick()
        {
            IsOn = !IsOn;
            
            handleTransform.DOAnchorPosX(IsOn ? handleX: handleX * -1 , 0.4f);

            handle.DOColor(IsOn ? onColor : offColor, 0.4f);
            background.DOColor(IsOn ? onColor : offColor, 0.4f);

            OnValueChangedIsOn?.Invoke(IsOn);
        }
    }
}
