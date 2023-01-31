using System;
using UnityEngine;

namespace Apollo11.Items.Crafting
{
    public class ItemsDoubleSlot : MonoBehaviour, IItemsSlots
    {
        [SerializeField] private Enums.Items Item1Type;
        [SerializeField] private Enums.Items Item2Type;
        [SerializeField] private int Item1Needed;
        [SerializeField] private int Item2Needed;
        
        public int Item1Count { get; private set; }
        public int Item2Count { get; private set; }

        public event Action OnFull;

        public void ReceiveItem(Enums.Items i)
        {
            if (!AcceptsItem(i)) return;

            if (i == Item1Type) Item1Count++;
            if (i == Item2Type) Item2Count++;

            if (Item1Count == Item1Needed && Item2Count == Item2Needed)
            {
                OnFull?.Invoke();
            }
        }
        
        public void Reset()
        {
            Item1Count = 0;
            Item2Count = 0;
        }

        public bool AcceptsItem(Enums.Items itemType)
        {
            if (itemType == Item1Type && Item1Count < Item1Needed) return true;
            if (itemType == Item2Type && Item2Count < Item2Needed) return true;

            return false;
        }

    }
}
