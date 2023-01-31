using System;

namespace Apollo11.Crafting
{
    public interface IOnProductionDone
    {
        public event Action OnProductionDone;
    }
}