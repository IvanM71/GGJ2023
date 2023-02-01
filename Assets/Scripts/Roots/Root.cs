namespace Apollo11.Roots
{
    public class Root
    {
        public Enums.RootStages stage;
        public Enums.RootType type;
        public Root(Enums.RootStages stage, Enums.RootType type)
        {
            this.stage = stage;
            this.type = type;
        }
    }
}
