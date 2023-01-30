using Apollo11.Interaction;
using Apollo11.Items;
using UnityEngine;

namespace Apollo11.Crafting
{
    public class FactoryController : MonoBehaviour, IInteractable, ICraftingInteraction
    {
        [SerializeField] private Factory factory;

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
            
        }
    }
}