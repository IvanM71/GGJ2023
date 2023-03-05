using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Apollo11
{
    public class ButtonWithDownEvent : Button
    {
        public event Action OnPress;
        public event Action OnRelease;

        public bool Pressed { get; private set; }

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            Pressed = true;
            OnPress?.Invoke();
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
            Pressed = false;
            OnRelease?.Invoke();
        }
    }
}
