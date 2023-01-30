using Apollo11.Interaction;
using UnityEngine;

namespace Apollo11.Crafting
{
    public class FactoryController : MonoBehaviour, IInteractable, IInteractionButtonHold
    {
        [SerializeField] private Factory factory;
        
        public void OnSelect()
        {
            
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

        public void OnInteractionStop()
        {
            
        }
    }
}