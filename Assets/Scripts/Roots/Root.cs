using UnityEngine;

namespace Apollo11.Roots
{
    public class Root
    {
        public Enums.RootStages Stage { get; set; }
        public Enums.RootType Type { get; set; }
        public Root(Enums.RootStages stage, Enums.RootType type)
        {
            Stage = stage;
            Type = type;
        }
    }
}
