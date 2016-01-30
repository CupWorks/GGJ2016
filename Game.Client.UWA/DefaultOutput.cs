using System;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Media;
using Game.Core;

namespace Game.Client.UWA
{
    public class DefaultOutput : IOutput
    {
        private Paragraph Paragraph { get; set; } = new Paragraph();

        public DefaultOutput(RichTextBlock richTextBlock)
        {
            richTextBlock.Blocks.Add(Paragraph);
        }

        public void Write(string text, OutputType type)
        {
            Paragraph.Inlines.Add(new Run {Text = text, Foreground = new SolidColorBrush(GetColor(type)) });
        }

        public void WriteLine(string text, OutputType type)
        {
            Paragraph.Inlines.Add(new Run { Text = text, Foreground = new SolidColorBrush(GetColor(type)) });
            Paragraph.Inlines.Add(new LineBreak());
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