using Apollo11.Items;
using UnityEngine;

namespace Apollo11.Player
{
    public class PlayerItemCarry : MonoBehaviour
    {
        [SerializeField] private Transform itemHolder;

        public bool IsHoldingItem { get; private set; }
        
        private Item _currentItem;


        public void TakeItem(Item item)
        {
            //TODO ad checks if can take
            if (IsHoldingItem) return;

            IsHoldingItem = true;
            _currentItem = item;
            item.transform.parent = itemHolder;
            item.transform.localPosition = Vector3.zero;
            item.transform.localRotation = Quaternion.Euler(Vector3.zero);
        }

        public void DropItem()
        {
            IsHoldingItem = false;
            _currentItem.transform.parent = null;
            _currentItem.transform.position = transform.position;
            _currentItem.transform.localRotation = Quaternion.Euler(Vector3.zero);

            _currentItem = null;
        }
    }
}
