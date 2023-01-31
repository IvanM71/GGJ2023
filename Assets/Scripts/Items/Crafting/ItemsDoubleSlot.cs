using System;
using Apollo11.Core;
using UnityEngine;

namespace Apollo11.Items.Crafting
{
    public class ItemsDoubleSlot : MonoBehaviour, IItemsSlots
    {
        [SerializeField] private Enums.Items Item1Type;
        [SerializeField] private Enums.Items Item2Type;
        [SerializeField] private int Item1Needed;
        [SerializeField] private int Item2Needed;
        
        [Space]
        [SerializeField] ItemsDoublePanel panel;
        
        public int Item1Count { get; private set; }
        public int Item2Count { get; private set; }

        public event Action OnFull;

        private void Awake()
        {
            Reset();
            var sprite1 = SystemsLocator.Inst.SO_ItemsPrefabs.Dictionary[Item1Type].IconSprite;
            var sprite2 = SystemsLocator.Inst.SO_ItemsPrefabs.Dictionary[Item2Type].IconSprite;
            panel.SetIcons(sprite1, sprite2);
        }

        public void ReceiveItem(Enums.Items i)
        {
            if (!AcceptsItem(i)) return;

            if (i == Item1Type) Item1Count++;
            if (i == Item2Type) Item2Count++;

            if (Item1Count == Item1Needed && Item2Count == Item2Needed)
            {
                OnFull?.Invoke();
            }
            panel.SetValues(Item1Count, Item1Needed, Item2Count, Item2Needed);
        }
        
        public void Reset()
        {
            Item1Count = 0;
            Item2Count = 0;
            panel.SetValues(0, Item1Needed, 0, Item2Needed);
        }

        public bool AcceptsItem(Enums.Items itemType)
        {
            if (itemType == Item1Type && Item1Count < Item1Needed) return true;
            if (itemType == Item2Type && Item2Count < Item2Needed) return true;

            return false;
        }
        

    }
}
