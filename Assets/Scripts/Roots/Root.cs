namespace Apollo11.Roots
{
    public class Root
    {
        public Enums.RootStages stage;
        public bool[] _growDirections; // 0 - up, 1 - down, 2 - left, 3 - right
        public Root(Enums.RootStages stage)
        {
            this.stage = stage;
            _growDirections = new bool[4] { false, false, false, false };
        }
    }
}
