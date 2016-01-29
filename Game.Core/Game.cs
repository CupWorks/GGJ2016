namespace Game.Core
{
    public class Game
    {
        private IInput Input { get; set; }
        private IOutput Output { get; set; }

        public Game(IInput input, IOutput output)
        {
            Input = input;
            Output = output;
        }

        public void Start()
        {
            Output.Write("Start game \\o/", OutputType.System);
        }
    }
}
