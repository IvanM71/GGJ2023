using System;
using System.Linq;
using Apollo11.Core;
using Apollo11.Roots;
using UnityEngine;

namespace Apollo11.Interaction
{
    public class InteractionSystem : MonoBehaviour
    {
        [SerializeField] private InteractionVision playersInteractionVision;
        [SerializeField] private GameObject interactionIcon;
        [SerializeField] private GameObject attackIcon;

        //public bool InteractionLocked { get; private set; }
        public Enums.PlayerInteractionState InteractionState { get; private set; }

        private ILongInteraction _currentLongInteractable;


        private void Update()
        {
            var inVision = playersInteractionVision.InteractablesInVision;

            IInteractable closest;
            switch (InteractionState)
            {
                case Enums.PlayerInteractionState.None:
                    var suitable1 = inVision.Where(
                            inter => inter.GetInteractableType() == Enums.InteractableObjectType.Item ||
                            inter.GetInteractableType() == Enums.InteractableObjectType.LongHoldAction)
                        .ToList();
                    closest = playersInteractionVision.FindClosestFromList(suitable1);
                    break;
                case Enums.PlayerInteractionState.HoldsItem:
                    var suitable2 = inVision.Where(
                            inter => inter.GetInteractableType() == Enums.InteractableObjectType.Crafter &&
                                ((ICraftingInteraction)inter).AcceptsItem(SystemsLocator.Inst.PlayerItemCarry.CurrentItem))
                        .ToList();
                    closest = playersInteractionVision.FindClosestFromList(suitable2);
                    break;
                case Enums.PlayerInteractionState.InLongInteraction:
                    closest = null;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            ShowIcon(closest);
            HandleInput(closest);
        }

        private void HandleInput(IInteractable closest)
        {
            if (InteractionState == Enums.PlayerInteractionState.InLongInteraction)
            {
                if (Input.GetKeyUp(KeyCode.E))
                {
                    UnlockInteraction();
                    return;
                }
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                SystemsLocator.Inst.PlayerItemCarry.DropItem();
                InteractionState = Enums.PlayerInteractionState.None;
                return;
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (closest == null) return;
                closest.OnInteractionStart();
                
                if (closest.GetInteractableType() == Enums.InteractableObjectType.LongHoldAction)
                    LockInteraction(closest);
                else if (closest.GetInteractableType() == Enums.InteractableObjectType.Item)
                    InteractionState = Enums.PlayerInteractionState.HoldsItem;
                else if (closest.GetInteractableType() == Enums.InteractableObjectType.Crafter)
                    InteractionState = Enums.PlayerInteractionState.None;
            }
            
        }

        private void LockInteraction(IInteractable obj)
        {
            InteractionState = Enums.PlayerInteractionState.InLongInteraction;
            _currentLongInteractable = (ILongInteraction)obj;
            SystemsLocator.Inst.PlayerMovement.LockMovement = true;
        }
        
        private void UnlockInteraction()
        {
            InteractionState = Enums.PlayerInteractionState.None;
            _currentLongInteractable.OnInteractionStop();
            _currentLongInteractable = null;
            SystemsLocator.Inst.PlayerMovement.LockMovement = false;
        }

        private void ShowIcon(IInteractable obj)
        {
            if (obj == null)
            {
                interactionIcon.SetActive(false);
                return;
            }
            
            var pos = obj.GetIconPosition();
            interactionIcon.transform.position = pos;
            interactionIcon.SetActive(true);
        }
        
    }
}
