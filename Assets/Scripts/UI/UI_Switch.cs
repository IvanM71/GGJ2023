using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Apollo11
{
    public class UI_Switch : MonoBehaviour
    {
        [SerializeField] private RawImage background;
        [SerializeField] private Image handle;
        [SerializeField] private RectTransform handleTransform;

        [SerializeField] private Color onColor;
        [SerializeField] private Color offColor;

        float handleX;

        bool isOn = true;

        private void Awake()
        {
            handleX = handleTransform.anchoredPosition.x;
        }

        public void OnClick()
        {
            isOn = !isOn;
            
            handleTransform.DOAnchorPosX(isOn ? handleX: handleX * -1 , 0.4f);

            handle.DOColor(isOn ? onColor : offColor, 0.4f);
            background.DOColor(isOn ? onColor : offColor, 0.4f);

            
        }
    }
}
