namespace Apollo11
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
