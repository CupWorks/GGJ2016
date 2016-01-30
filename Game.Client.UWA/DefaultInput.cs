using System;
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

        public async void Request()
        {
            await TextBox.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                TextBox.IsEnabled = true;
            });
        }
    }
}