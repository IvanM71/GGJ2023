using Apollo11.Items;
using UnityEngine;

namespace Apollo11.Player
{
    public class PlayerItemCarry : MonoBehaviour
    {
        [SerializeField] private Transform itemHolder;

        public bool IsHoldingItem { get; private set; }

        public Item CurrentItem { get; set; }


        public void TakeItem(Item item)
        {
            //TODO ad checks if can take
            if (IsHoldingItem) return;

            IsHoldingItem = true;
            CurrentItem = item;
            item.transform.parent = itemHolder;
            item.transform.localPosition = Vector3.zero;
            item.transform.localRotation = Quaternion.Euler(Vector3.zero);
        }

        public void DropItem()
        {
            IsHoldingItem = false;
            CurrentItem.transform.parent = null;
            CurrentItem.transform.position = transform.position;
            CurrentItem.transform.localRotation = Quaternion.Euler(Vector3.zero);

            CurrentItem = null;
        }
    }
}
