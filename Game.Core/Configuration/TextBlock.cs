using System.Xml.Serialization;

namespace Game.Core.Configuration
{
    public class TextBlock
    {
        [XmlText]
        public string Content { get; set; } = "";
        [XmlAttribute]
        public int Delay { get; set; }
    }
}
