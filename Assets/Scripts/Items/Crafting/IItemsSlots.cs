using System;

namespace Apollo11.Items.Crafting
{
    public interface IItemsSlots
    {
        public event Action OnFull;

        public void ReceiveItem(Enums.Items i);

        public void Reset();

        public bool AcceptsItem(Enums.Items itemType);
        public bool AcceptsItem(Item item) => AcceptsItem(item.ItemType);
    }
}