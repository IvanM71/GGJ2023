using System;
using UnityEngine;

namespace Apollo11.Items.Crafting
{
    public class ItemsSingleSlot : MonoBehaviour, IItemsSlots
    {
        [SerializeField] private Enums.Items Item1Type;
        [SerializeField] private int Item1Needed;

        public int Item1Count { get; private set; }

        public event Action OnFull;

        public void ReceiveItem(Enums.Items i)
        {
            if (!AcceptsItem(i)) return;

            if (i == Item1Type) Item1Needed++;

            if (Item1Count == Item1Needed)
            {
                OnFull?.Invoke();
            }
        }
        
        public void Reset()
        {
            Item1Count = 0;
        }

        public bool AcceptsItem(Enums.Items itemType)
        {
            if (itemType == Item1Type && Item1Count < Item1Needed) return true;

            return false;
        }

    }
}
