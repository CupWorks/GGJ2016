namespace Game.Core
{
    public class Game
    {
        private IInput Input { get; set; }
        private IOutput Output { get; set; }

        public bool IsRunning { get; set; } = false;

        public Game(IInput input, IOutput output)
        {
            Input = input;
            Input.OnTextReceived += InputOnOnTextReceived;
            Output = output;
        }

        private void InputOnOnTextReceived(string text)
        {
            Output.Write("... " + text, OutputType.Normal);
        }

        public void Start()
        {
            IsRunning = true;
            Output.Write("Start game \\o/", OutputType.System);
        }
    }
}
