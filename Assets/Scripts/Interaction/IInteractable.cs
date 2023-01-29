using UnityEngine;

namespace Apollo11.Interaction
{
    public interface IInteractionTarget
    {
        void AtInteractionTargetSelected();
    }
    
    public interface IInteractionButtonHold
    {
        public void OnButtonUp();
    }

    public interface IInteractable
    {
        public Vector2 GetIconOffset();
        public Vector2 GetPosition();
        public void OnButtonDown();
    }
}