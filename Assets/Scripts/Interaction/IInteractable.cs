using Apollo11.Items;
using Apollo11.Roots;

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

    public interface IInteractable : IGetPosition, IGetIconPosition
    {
        public Enums.InteractableObjectType GetInteractableType();

        public void OnInteractionStart();
    }
}