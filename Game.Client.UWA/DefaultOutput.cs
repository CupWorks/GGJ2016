using System;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Media;
using Game.Core;

namespace Game.Client.UWA
{
    public class DefaultOutput : IOutput
    {
        private RichTextBlock RichTextBlock { get; }
        private Paragraph Paragraph { get; } = new Paragraph();

        public DefaultOutput(RichTextBlock richTextBlock)
        {
            RichTextBlock = richTextBlock;
            RichTextBlock.Blocks.Add(Paragraph);
        }

        public async void Write(string text, OutputType type)
        {
            await RichTextBlock.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                Paragraph.Inlines.Add(new Run { Text = text, Foreground = new SolidColorBrush(GetColor(type)) });
                ScrollToEnd();
            });
        }

        public async void WriteLine(string text, OutputType type)
        {
            await RichTextBlock.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                Paragraph.Inlines.Add(new Run { Text = text, Foreground = new SolidColorBrush(GetColor(type)) });
                Paragraph.Inlines.Add(new LineBreak());
                ScrollToEnd();
            });
        }

        private void ScrollToEnd()
        {
            var scrollViewer = RichTextBlock.Parent as ScrollViewer;
            scrollViewer?.ScrollToVerticalOffset(scrollViewer.ActualHeight);
        }

        private Color GetColor(OutputType type)
        {
            switch (type)
            {
                case OutputType.System:
                    return Colors.Cyan;
                case OutputType.Normal:
                    return Colors.White;
                case OutputType.Action:
                    return Colors.Green;
                case OutputType.Warning:
                    return Colors.Yellow;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}