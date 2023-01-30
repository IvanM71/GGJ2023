using Apollo11.Items;
using UnityEngine;

namespace Apollo11.Interaction
{
    public interface IInteractionTarget
    {
        void AtInteractionTargetSelected();
    }
    
    public interface ILongInteraction
    {
        public void OnInteractionStop();
    }
    
    public interface ICraftingInteraction
    {
        public bool AcceptsItem(Item item);
    }

    public interface IInteractable
    {
        public Enums.InteractableObjectType GetInteractableType();
        public Vector2 GetIconOffset();
        public Vector2 GetPosition();
        public void OnInteractionStart();
    }
}