using System;
using Windows.Storage;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;
using Game.Core;


namespace Game.Client.UWA
{
    public class DefaultInput : IInput
    {
        public DefaultInput(TextBox textBox)
        {
            textBox.TextChanged += (sender, args) =>
            {
                var text = (sender as TextBox)?.Text;
                if (string.IsNullOrEmpty(text)) return;
                Read(text);
            };
        }

        public event InputEvent OnTextReceived;
        public void Read(string text)
        {
            OnTextReceived?.Invoke(text);
        }
    }

    public class DefaultOutput : IOutput
    {
        private Paragraph Paragraph { get; set; } = new Paragraph();

        public DefaultOutput(RichTextBlock richTextBlock)
        {
            richTextBlock.Blocks.Add(Paragraph);
        }

        public void Write(string text, OutputType type)
        {
            Paragraph.Inlines.Add(new Run {Text = text});
        }

        public void WriteLine(string text, OutputType type)
        {
            Paragraph.Inlines.Add(new Run { Text = text });
        }
    }

    public class SoundManager : ISoundManager
    {
        public void PlaySound(string soundFile)
        {
        }

        public void PlayLoop(string soundFile)
        {
        }
    }

    public sealed partial class MainPage : Page
    {
        private IInput Input { get; set; }
        private IOutput Output { get; set; }
        private ISoundManager SoundManager { get; set; }
        private Core.Game Game { get; set; }

        public MainPage()
        {
            InitializeComponent();

            Input = new DefaultInput(TextBox);
            Output = new DefaultOutput(RichTextBlock);
            SoundManager = new SoundManager();

            var local = ApplicationData.Current.LocalFolder;
            var folders = local.GetFoldersAsync().GetResults();

            Game = new Core.Game(Input, Output, SoundManager, null, null);
        }
    }
}
