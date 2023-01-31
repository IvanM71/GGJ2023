using System;
using Apollo11.Core;
using UnityEngine;

namespace Apollo11.Items.Crafting
{
    public class ItemsSingleSlot : MonoBehaviour, IItemsSlots
    {
        [SerializeField] private Enums.Items Item1Type;
        [SerializeField] private int Item1Needed;
        [Space]
        [SerializeField] ItemsSinglePanel panel;

        public int Item1Count { get; private set; }

        public event Action OnFull;

        private void Awake()
        {
            Reset();
            var sprite1 = SystemsLocator.Inst.SO_ItemsPrefabs.Dictionary[Item1Type].IconSprite;
            panel.SetIcons(sprite1);
        }

        public void ReceiveItem(Enums.Items i)
        {
            if (!AcceptsItem(i)) return;

            if (i == Item1Type) Item1Needed++;

            if (Item1Count == Item1Needed)
            {
                OnFull?.Invoke();
            }
            panel.SetValues(Item1Count, Item1Needed);
        }
        
        public void Reset()
        {
            Item1Count = 0;
            panel.SetValues(0, Item1Needed);
        }

        public bool AcceptsItem(Enums.Items itemType)
        {
            if (itemType == Item1Type && Item1Count < Item1Needed) return true;

            return false;
        }

        public void TogglePanelVisible(bool b)
        {
            panel.gameObject.SetActive(b);
        }
    }
}
