using Apollo11.Interaction;
using Apollo11.Items;
using UnityEngine;

namespace Apollo11.Crafting
{
    public class ExtractorController : MonoBehaviour, IInteractable, IInteractionButtonHold
    {
        [SerializeField] private Extractor extractor;
        

        public Vector2 GetIconOffset()
        {
            return new Vector2(0, 0.5f);
        }

        public Vector2 GetPosition()
        {
            return transform.position;
        }

        public void OnButtonDown()
        {
            extractor.StartExtraction();
        }

        public void OnButtonUp()
        {
            extractor.StopExtraction();
        }
    }
}