using UnityEngine;

namespace Apollo11
{
    public interface IInteractable
    {
        public void OnSelect();
        public Vector2 GetIconOffset();
        public Vector2 GetPosition();
        public void OnButtonDown();
        public void OnButtonUp();
    }
}