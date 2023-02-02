using Apollo11.Core;
using Apollo11.Interaction;
using Apollo11.Items;
using UnityEngine;

namespace Apollo11.Crafting
{
    public class FactoryController : MonoBehaviour, IInteractable, ICraftingInteraction, IInPlayerVision
    {
        [SerializeField] private Factory factory;
        [SerializeField] private Vector2 interactionIconOffset = new(0f, 0.65f);

        public Enums.InteractableObjectType GetInteractableType() => Enums.InteractableObjectType.Crafter;

        public bool AcceptsItem(Item item)
        {
            return factory.ItemsSlots.AcceptsItem(item);
        }

        public Vector2 GetIconPosition() => GetPosition() + interactionIconOffset;

        public Vector2 GetPosition() => transform.position;

        public void OnInteractionStart()
        {
            ReceiveItem(SystemsLocator.Inst.PlayerSystems.PlayerItemCarry.DeleteItemFromHands());
        }

        private void ReceiveItem(Enums.Items itemType)
        {
            factory.ItemsSlots.ReceiveItem(itemType);
        }

        public void AtPlayerVisionEnter()
        {
            factory.ItemsSlots.TogglePanelVisible(true);
        }

        public void AtPlayerVisionExit()
        {
            factory.ItemsSlots.TogglePanelVisible(false);
        }
    }
}