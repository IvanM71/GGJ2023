using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Apollo11
{
    public class ButtonWithDownEvent : Button
    {
        public event Action OnPressDown;
        
        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            OnPressDown?.Invoke();
        }
    }
}
