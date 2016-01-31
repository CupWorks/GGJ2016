using System;
using System.IO;
using System.Linq;
using Windows.ApplicationModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace Game.Client.UWA
{
    public sealed partial class MainPage : Page
    {
        private DefaultInput Input { get; set; }
        private DefaultOutput Output { get; set; }
        private SoundManager SoundManager { get; set; }
        private Core.Game Game { get; set; }
        private bool IsHistoryVisible { get; set; }

        public MainPage()
        {
            InitializeComponent();
            TextBox.IsEnabled = false;
            Loaded += OnLoaded;
        }

        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            Input = new DefaultInput(TextBox);
            HistoryListBox.ItemsSource = Input.LastCommands;
            Output = new DefaultOutput(RichTextBlock);
            SoundManager = new SoundManager();

            var filesFolder = await Package.Current.InstalledLocation.GetFolderAsync("Files");
            var commandsStream = await filesFolder.OpenStreamForReadAsync("Commands.xml");
            var storyStepsStream = await filesFolder.OpenStreamForReadAsync("StorySteps.xml");
            var audioFilesStream = await filesFolder.OpenStreamForReadAsync("Sounds.xml");

            Game = new Core.Game(
                Input,
                Output,
                SoundManager,
                audioFilesStream,
                commandsStream,
                storyStepsStream);
            Game.Start();

            TextBox.IsEnabled = true;
        }

        private void HistoryOnClick(object sender, RoutedEventArgs e)
        {
            var columnDefinition = RootGrid.ColumnDefinitions.Skip(1).FirstOrDefault();
            if (columnDefinition == null) return;
            columnDefinition.Width = IsHistoryVisible ? new GridLength(1, GridUnitType.Star) : new GridLength(0);
            IsHistoryVisible = !IsHistoryVisible;
        }

        private void HistoryItemOnTapped(object sender, TappedRoutedEventArgs e)
        {
            var textBlock = sender as TextBlock;
            if(textBlock == null) return;
            Input.Push(textBlock.Text);
        }
    }
}
