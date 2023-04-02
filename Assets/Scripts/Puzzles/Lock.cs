using Apollo11.Core;
using Apollo11.Interaction;
using Apollo11.Items;
using Apollo11.Puzzles;
using UnityEngine;

namespace Apollo11
{
    public class Lock : APuzzle, IInteractable, ICraftingInteraction
    {
        [SerializeField] private GameObject KeyIn;
        [SerializeField] private Vector2 interactionIconOffset = new(0f, 0.4f);
        public Enums.InteractableObjectType GetInteractableType() => Enums.InteractableObjectType.Crafter;

        public bool AcceptsItem(Item item)
        {
            return item.ItemType == Enums.Items.Key;
        }

        public Vector2 GetIconPosition() => GetPosition() + interactionIconOffset;

        public Vector2 GetPosition() => transform.position;

        public void OnInteractionStart()
        {
            ReceiveItem(SystemsLocator.Inst.PlayerSystems.PlayerItemCarry.DeleteItemFromHands());
        }

        private void ReceiveItem(Enums.Items itemType)
        {
            if (itemType != Enums.Items.Key) return;

            KeyIn.SetActive(true);
            OnSolved?.Invoke();
            SystemsLocator.Inst.SoundController.PlayItemIn();
        }
    }
}
