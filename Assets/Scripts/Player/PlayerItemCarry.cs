using System;
using Apollo11.Core;
using Apollo11.Items;
using UnityEngine;

namespace Apollo11.Player
{
    public class PlayerItemCarry : MonoBehaviour
    {
        [SerializeField] private Transform itemHolder;

        public bool IsHoldingItem { get; private set; }

        public Item CurrentItem { get; private set; }
        public int CurrentItemAmount { get; private set; }

        public event Action<int> OnItemsCountChanged; 


        public void TakeItem(Item item)
        {
            if (IsHoldingItem && CurrentItem.ItemType!=item.ItemType)
                return;
            
            SystemsLocator.Inst.PlayerSystems.InteractionVision.InteractablesInVision.Remove(item);
            SystemsLocator.Inst.SoundController.PlayPickItem();

            if (IsHoldingItem)
            {
                CurrentItemAmount++;
                Destroy(item.gameObject);
            }
            else
            {
                CurrentItemAmount = 1;
                IsHoldingItem = true;
                CurrentItem = item;
                item.transform.parent = itemHolder;
                item.transform.localPosition = Vector3.zero;
                item.transform.localRotation = Quaternion.Euler(Vector3.zero);
            }
            
            OnItemsCountChanged?.Invoke(CurrentItemAmount);
        }
        
        
        /// <returns>Hand is empty after drop</returns>
        public bool DropItem()
        {
            if (!IsHoldingItem) return true;

            SystemsLocator.Inst.SoundController.PlayThrowItem();
            
            if (CurrentItemAmount == 1)
            {
                CurrentItemAmount = 0;
                IsHoldingItem = false;
                CurrentItem.transform.parent = null;
                CurrentItem.transform.position = transform.position;
                CurrentItem.transform.localRotation = Quaternion.Euler(Vector3.zero);

                CurrentItem = null;
                
                OnItemsCountChanged?.Invoke(CurrentItemAmount);
                return true;
            }
            else
            {
                CurrentItemAmount--;
                var clone = Instantiate(CurrentItem);
                clone.transform.parent = null;
                clone.transform.position = transform.position;
                clone.transform.localRotation = Quaternion.Euler(Vector3.zero);
                
                OnItemsCountChanged?.Invoke(CurrentItemAmount);
                return false;
            }

        }

        public Enums.Items DeleteItemFromHands()
        {
            var res = Enums.Items.Unknown;
            if (!IsHoldingItem) return res;
            
            res = CurrentItem.ItemType;

            if (CurrentItemAmount == 1)
            {
                res = CurrentItem.ItemType;

                CurrentItemAmount = 0;
                IsHoldingItem = false;
                Destroy(CurrentItem.gameObject);
                CurrentItem = null;
            }
            else
            {
                CurrentItemAmount--;
            }
            
            OnItemsCountChanged?.Invoke(CurrentItemAmount);

            return res;
        }
    }
}
