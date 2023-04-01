using System;
using System.Collections.Generic;
using System.Linq;
using Apollo11.Core;
using Apollo11.Items;
using Apollo11.Roots;
using UnityEngine;

namespace Apollo11.Interaction
{
    public class InteractionSystem : MonoBehaviour
    {
        [SerializeField] private GameObject interactionIcon;
        [SerializeField] private AttackIcon attackIcon;

        private Enums.PlayerInteractionState InteractionState { get; set; }
        public bool InAttack { get; set; }

        private InteractionVision _playersInteractionVision;
        private ILongInteraction _currentLongInteractable;

        private void Awake()
        {
            _playersInteractionVision = SystemsLocator.Inst.PlayerSystems.InteractionVision;
            attackIcon.ToggleShow(false);
        }

        private void LateUpdate()
        {
            var interactablesInVision = _playersInteractionVision.InteractablesInVision;
            var damagablesInVision = _playersInteractionVision.DamagablesInVision;

            IInteractable closestInteractable = null;
            IDamagable closestDamagable = null;

            if (InAttack)
            {
                ShowIcons(null, null);
                return;
            }
            
            switch (InteractionState)
            {
                case Enums.PlayerInteractionState.None:
                    var suitable1 = interactablesInVision.Where(
                            inter => inter.GetInteractableType() == Enums.InteractableObjectType.Item ||
                            inter.GetInteractableType() == Enums.InteractableObjectType.LongHoldAction ||
                            inter.GetInteractableType() == Enums.InteractableObjectType.Action)
                        .ToList();
                    closestInteractable = _playersInteractionVision.FindClosestFromList(suitable1);
                    closestDamagable = FindPossibleDamagable(damagablesInVision);
                    break;
                case Enums.PlayerInteractionState.HoldsItem:
                    var currentItemInHand = SystemsLocator.Inst.PlayerSystems.PlayerItemCarry.CurrentItem;
                    var suitable2 = interactablesInVision.Where(
                            inter => 
                                (inter.GetInteractableType() == Enums.InteractableObjectType.Crafter &&
                                ((ICraftingInteraction)inter).AcceptsItem(currentItemInHand))
                                || 
                                (inter.GetInteractableType() == Enums.InteractableObjectType.Item && 
                                ((Item)inter).ItemType == currentItemInHand.ItemType)
                            )
                        .ToList();
                    closestInteractable = _playersInteractionVision.FindClosestFromList(suitable2);
                    closestDamagable = null;
                    break;
                case Enums.PlayerInteractionState.InLongInteraction:
                    closestInteractable = null;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            ShowIcons(closestInteractable, closestDamagable);
            HandleInput(closestInteractable, closestDamagable);
            SystemsLocator.Inst.GameCanvas.TouchControls.ResetFlags();
        }

        private IDamagable FindPossibleDamagable(List<IDamagable> list)
        {
            var chargesSystem = SystemsLocator.Inst.WeaponsCharges;
            var suitable = list.Where(d => chargesSystem.EnoughCharges(d.GetWeapon(), 1)).ToList();
            var res = _playersInteractionVision.FindClosestFromList(suitable);
            return res;
        }

        private void ShowIcons(IInteractable closestInteractable, IDamagable closestDamagable)
        {
            if (closestInteractable == null)
            {
                interactionIcon.SetActive(false);
            }
            else
            {
                var pos = closestInteractable.GetIconPosition();
                interactionIcon.transform.position = pos;
                interactionIcon.SetActive(true);
            }
            
            if (closestDamagable == null)
            {
                attackIcon.ToggleShow(false);
            }
            else
            {
                var pos = closestDamagable.GetIconPosition();
                attackIcon.Place(closestDamagable.GetWeapon(), pos);
                attackIcon.ToggleShow(true);
            }
            
           
        }

        private void HandleInput(IInteractable closestInteractable, IDamagable closestDamagable)
        {
            if (InAttack || SystemsLocator.Inst.InPause) return;

            var GetKeyDownE = Input.GetKeyDown(KeyCode.E) || SystemsLocator.Inst.GameCanvas.TouchControls.PressedEThisFrame;
            var GetKeyDownF = Input.GetKeyDown(KeyCode.F) || SystemsLocator.Inst.GameCanvas.TouchControls.PressedFThisFrame;
            var GetKeyDownR = Input.GetKeyDown(KeyCode.R) || SystemsLocator.Inst.GameCanvas.TouchControls.PressedRThisFrame;
            
            var GetKeyUpE = Input.GetKeyUp(KeyCode.E) || SystemsLocator.Inst.GameCanvas.TouchControls.ReleasedEThisFrame;

            if (InteractionState == Enums.PlayerInteractionState.InLongInteraction)
            {
                if (GetKeyUpE)
                {
                    UnlockInteraction();
                    return;
                }
            }

            if (GetKeyDownF)
            {
                SystemsLocator.Inst.AttackSystem.TryAttack(closestDamagable);
                return;
            }

            if (GetKeyDownR)
            {
                var handIsEmpty = SystemsLocator.Inst.PlayerSystems.PlayerItemCarry.DropItem();
                if (handIsEmpty)
                    InteractionState = Enums.PlayerInteractionState.None;
                
                return;
            }

            if (GetKeyDownE)
            {
                if (closestInteractable == null)
                    return;
                closestInteractable.OnInteractionStart();

                switch (closestInteractable.GetInteractableType())
                {
                    case Enums.InteractableObjectType.LongHoldAction:
                        LockInteraction(closestInteractable);
                        break;
                    case Enums.InteractableObjectType.Item:
                        InteractionState = Enums.PlayerInteractionState.HoldsItem;
                        break;
                    case Enums.InteractableObjectType.Crafter:
                        if (SystemsLocator.Inst.PlayerSystems.PlayerItemCarry.CurrentItemAmount == 0)
                        {
                            InteractionState = Enums.PlayerInteractionState.None;
                        }
                        break;
                    case Enums.InteractableObjectType.Action:
                        break;
                    case Enums.InteractableObjectType.Inactive:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

            }

            
        }

        private void LockInteraction(IInteractable obj)
        {
            InteractionState = Enums.PlayerInteractionState.InLongInteraction;
            _currentLongInteractable = (ILongInteraction)obj;
            SystemsLocator.Inst.PlayerSystems.PlayerMovement.LockMovement = true;
        }
        
        private void UnlockInteraction()
        {
            InteractionState = Enums.PlayerInteractionState.None;
            _currentLongInteractable.OnInteractionStop();
            _currentLongInteractable = null;
            SystemsLocator.Inst.PlayerSystems.PlayerMovement.LockMovement = false;
        }

        private void ShowIcon(IInteractable obj)
        {
            
        }
        
    }
}
