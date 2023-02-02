using Apollo11.Core;
using Apollo11.Interaction;
using UnityEngine;

namespace Apollo11.Crafting
{
    public class ExtractorController : MonoBehaviour, IInteractable, ILongInteraction
    {
        [SerializeField] private Extractor extractor;
        [SerializeField] private Enums.HandWeapon extractionWeapon;
        [Space]
        [SerializeField] private Vector2 interactionIconOffset = new(0f, 0.5f);


        public Enums.InteractableObjectType GetInteractableType() => Enums.InteractableObjectType.LongHoldAction;


        public Vector2 GetIconPosition() => GetPosition() + interactionIconOffset;
        public Vector2 GetPosition() => transform.position;

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