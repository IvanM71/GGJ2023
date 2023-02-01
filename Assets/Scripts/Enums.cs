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
        
        public enum RootWeapon
        {
            Unknown,
            Axe,
            Saw,
            Sprayer
        }

        public enum RootType
        {
            Unknown,
            TypeA,
            TypeB,
            TypeC
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

        public enum RootStages
        {
            STAGE_0, 
            STAGE_1, 
            STAGE_2, 
            STAGE_3, 
            MAIN
        }
    }
}