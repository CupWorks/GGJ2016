using System;
using System.Collections.ObjectModel;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Game.Core;

namespace Game.Client.UWA
{
    public class DefaultInput : IInput
    {
        private TextBox TextBox { get; }

        public event InputEvent OnTextReceived;
        public ObservableCollection<string> LastCommands { get; set; } = new ObservableCollection<string>();

        public DefaultInput(TextBox textBox)
        {
            TextBox = textBox;
            TextBox.KeyDown += OnKeyDown;
        }

        private void OnKeyDown(object sender, KeyRoutedEventArgs keyRoutedEventArgs)
        {
            if (keyRoutedEventArgs.Key != VirtualKey.Enter) return;

            OnTextReceived?.Invoke(TextBox.Text);
            if (!LastCommands.Contains(TextBox.Text))
            {
                LastCommands.Add(TextBox.Text);
            }
            TextBox.Text = "";
            TextBox.IsEnabled = false;
        }

        public void Push(string text)
        {
            OnTextReceived?.Invoke(text);
            TextBox.IsEnabled = false;
        }

        public async void Request()
        {
            await TextBox.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                TextBox.IsEnabled = true;
            });
        }
    }
}