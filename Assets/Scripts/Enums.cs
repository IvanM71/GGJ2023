namespace Apollo11
{
    public static class Enums
    {
        public enum Items
        {
            Unknown,
            Root,
            Coal,
            IronOre,
            Iron
            
        }
        
        public enum HandWeapon
        {
            Unknown,
            Axe,
            Saw,
            Shears,
            Pickaxe,
            Sprayer

        }
        
        public enum PlayerInteractionState
        {
            None,
            HoldsItem,
            InLongInteraction
        }
        
        public enum InteractableObjectType
        {
            Item,
            LongHoldAction,
            Crafter
        }
    }
}