using Apollo11.Core;
using Apollo11.Interaction;
using Apollo11.Items;
using UnityEngine;

namespace Apollo11.Crafting
{
    public class ArmoryController : MonoBehaviour, IInteractable, ICraftingInteraction
    {
        [SerializeField] private Armory armory;


        public Enums.InteractableObjectType GetInteractableType() => Enums.InteractableObjectType.Crafter;
        public bool AcceptsItem(Item item)
        {
            return true; //TODO
        }

        public Vector2 GetIconOffset()
        {
            return new Vector2(0, 0.5f);
        }

        public Vector2 GetPosition()
        {
            return transform.position;
        }

        public void OnInteractionStart()
        {
            ReceiveItem(SystemsLocator.Inst.PlayerItemCarry.DeleteItemFromHands());
        }

        private void ReceiveItem(Enums.Items itemType)
        {
            
        }
    }
}