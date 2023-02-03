using System;

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
            Iron,
            MagicOre,
            MagicIron,
            Leave,
            Biomass,
            Water

        }
        
        public enum HandWeapon
        {
            Unknown,
            Axe,
            Saw,
            Shears,
            Pickaxe,
            Sprayer,
            Bucket
        }
        
        public enum RootWeapon
        {
            Unknown,
            Axe,
            Saw,
            Sprayer
        }

        public static HandWeapon RootWeaponToHandWeapon(RootWeapon rw)
        {
            switch (rw)
            {
                case RootWeapon.Unknown:
                    return HandWeapon.Unknown;
                case RootWeapon.Axe:
                    return HandWeapon.Axe;
                case RootWeapon.Saw:
                    return HandWeapon.Saw;
                case RootWeapon.Sprayer:
                    return HandWeapon.Sprayer;
                default:
                    throw new ArgumentOutOfRangeException(nameof(rw), rw, null);
            }
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