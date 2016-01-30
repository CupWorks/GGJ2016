using System.Xml.Serialization;

namespace Source.Configuration
{
    public class TextBlock
    {
        [XmlText]
        public string Content { get; set; }
        [XmlAttribute]
        public int Delay { get; set; }

        public TextBlock()
        {
            Content = "";
        }
    }
}
