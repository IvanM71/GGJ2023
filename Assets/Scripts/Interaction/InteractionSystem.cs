using Apollo11.Core;
using UnityEngine;

namespace Apollo11.Interaction
{
    public class InteractionSystem : MonoBehaviour
    {
        [SerializeField] private InteractionVision playersInteractionVision;
        [SerializeField] private GameObject interactionIcon;

        public bool InteractionLocked { get; private set; }

        private IInteractable _lockedInteractable;

        private void Update()
        {
            var closest = playersInteractionVision.ClosestInteractable;
            if (!InteractionLocked)
            {
                if (closest == null)
                {
                    HideIcon();
                    return;
                }
            
                UpdateIcon(closest);
            }

            
            if (Input.GetKeyDown(KeyCode.E) && !InteractionLocked)
            {
                closest.OnInteractionStart();
                if (closest is IInteractionButtonHold)
                    LockInteraction(closest);
            }
            else if (Input.GetKeyUp(KeyCode.E) && InteractionLocked)
            {
                ((IInteractionButtonHold)_lockedInteractable).OnInteractionStop();
                UnlockInteraction();
            }
        }

        private void LockInteraction(IInteractable obj)
        {
            InteractionLocked = true;
            _lockedInteractable = obj;
            HideIcon();
            SystemsLocator.Inst.PlayerMovement.LockMovement = true;
        }
        
        private void UnlockInteraction()
        {
            InteractionLocked = false;
            SystemsLocator.Inst.PlayerMovement.LockMovement = false;
        }

        private void UpdateIcon(IInteractable obj)
        {
            var pos = obj.GetPosition() + obj.GetIconOffset();
            interactionIcon.transform.position = pos;
            interactionIcon.SetActive(true);
        }
        
        private void HideIcon()
        {
            interactionIcon.SetActive(false);
        }
    }
}
