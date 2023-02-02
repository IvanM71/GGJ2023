using System;
using System.Collections.Generic;
using System.Linq;
using Apollo11.Core;
using Apollo11.Roots;
using UnityEngine;

namespace Apollo11.Interaction
{
    public class InteractionSystem : MonoBehaviour
    {
        [SerializeField] private GameObject interactionIcon;
        [SerializeField] private AttackIcon attackIcon;
        
        public Enums.PlayerInteractionState InteractionState { get; private set; }
        public bool InAttack { get; private set; }

        private InteractionVision _playersInteractionVision;
        private ILongInteraction _currentLongInteractable;

        private void Awake()
        {
            _playersInteractionVision = SystemsLocator.Inst.PlayerSystems.InteractionVision;
            attackIcon.ToggleShow(false);
        }

        private void Update()
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
                            inter.GetInteractableType() == Enums.InteractableObjectType.LongHoldAction)
                        .ToList();
                    closestInteractable = _playersInteractionVision.FindClosestFromList(suitable1);
                    closestDamagable = FindPossibleDamagable(damagablesInVision);
                    break;
                case Enums.PlayerInteractionState.HoldsItem:
                    var suitable2 = interactablesInVision.Where(
                            inter => inter.GetInteractableType() == Enums.InteractableObjectType.Crafter &&
                                ((ICraftingInteraction)inter).AcceptsItem(SystemsLocator.Inst.PlayerSystems.PlayerItemCarry.CurrentItem))
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
            if (InAttack) return;

            if (InteractionState == Enums.PlayerInteractionState.InLongInteraction)
            {
                if (Input.GetKeyUp(KeyCode.E))
                {
                    UnlockInteraction();
                    return;
                }
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                
                SystemsLocator.Inst.AttackSystem.TryAttack(closestDamagable);
                return;
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                SystemsLocator.Inst.PlayerSystems.PlayerItemCarry.DropItem();
                InteractionState = Enums.PlayerInteractionState.None;
                return;
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (closestInteractable == null) return;
                closestInteractable.OnInteractionStart();
                
                if (closestInteractable.GetInteractableType() == Enums.InteractableObjectType.LongHoldAction)
                    LockInteraction(closestInteractable);
                else if (closestInteractable.GetInteractableType() == Enums.InteractableObjectType.Item)
                    InteractionState = Enums.PlayerInteractionState.HoldsItem;
                else if (closestInteractable.GetInteractableType() == Enums.InteractableObjectType.Crafter)
                    InteractionState = Enums.PlayerInteractionState.None;
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
