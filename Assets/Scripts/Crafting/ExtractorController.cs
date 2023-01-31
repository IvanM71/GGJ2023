using Apollo11.Core;
using Apollo11.Interaction;
using UnityEngine;

namespace Apollo11.Crafting
{
    public class ExtractorController : MonoBehaviour, IInteractable, ILongInteraction
    {
        [SerializeField] private Extractor extractor;
        [SerializeField] private Enums.HandWeapon extractionWeapon;


        public Enums.InteractableObjectType GetInteractableType() => Enums.InteractableObjectType.LongHoldAction;

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
            extractor.StartExtraction();
            SystemsLocator.Inst.PlayerAnimation.PlayHandWeapon(extractionWeapon);
        }

        public void OnInteractionStop()
        {
            extractor.StopExtraction();
            SystemsLocator.Inst.PlayerAnimation.StopHandWeapon();
        }
    }
}