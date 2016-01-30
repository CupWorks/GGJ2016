using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Windows.ApplicationModel;
using Windows.Storage;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Game.Core;


namespace Game.Client.UWA
{
    public class DefaultInput : IInput
    {
        private TextBox TextBox { get; }

        public event InputEvent OnTextReceived;

        public DefaultInput(TextBox textBox)
        {
            TextBox = textBox;
            TextBox.KeyDown += OnKeyDown;
        }

        private void OnKeyDown(object sender, KeyRoutedEventArgs keyRoutedEventArgs)
        {
            if (keyRoutedEventArgs.Key != VirtualKey.Enter) return;

            OnTextReceived?.Invoke(TextBox.Text);
            TextBox.Text = "";
            TextBox.IsEnabled = false;
        }

        public void Request()
        {
            TextBox.IsEnabled = true;
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
            Paragraph.Inlines.Add(new LineBreak());
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
            TextBox.IsEnabled = false;
            Loaded += OnLoaded;
        }

        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            Input = new DefaultInput(TextBox);
            Output = new DefaultOutput(RichTextBlock);
            SoundManager = new SoundManager();

            var filesFolder = await Package.Current.InstalledLocation.GetFolderAsync("Files");
            var commandsStream = await filesFolder.OpenStreamForReadAsync("Commands.xml");
            var storyStepsStream = await filesFolder.OpenStreamForReadAsync("StorySteps.xml");

            Game = new Core.Game(Input, Output, SoundManager, commandsStream, storyStepsStream);
            Game.Start();

            TextBox.IsEnabled = true;
        }
    }
}
