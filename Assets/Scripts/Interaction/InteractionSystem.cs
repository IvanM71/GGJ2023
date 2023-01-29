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
            //TODO player cant move
            print("DONT MOVE");
        }
        
        private void UnlockInteraction()
        {
            InteractionLocked = false;
            //TODO player can move
            print("MOVE");
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
