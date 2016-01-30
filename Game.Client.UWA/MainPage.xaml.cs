using System;
using System.IO;
using Windows.ApplicationModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Game.Core;


namespace Game.Client.UWA
{
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
