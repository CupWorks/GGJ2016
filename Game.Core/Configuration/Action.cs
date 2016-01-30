using System.Collections.Generic;
using System.Xml.Serialization;

namespace Game.Core.Configuration
{
    public class Action
    {
        [XmlAttribute]
        public string Command { get; set; } = "";
        [XmlAttribute]
        public string NextStep { get; set; } = "";
        [XmlElement("Text")]
        public List<TextBlock> Text { get; set; } = new List<TextBlock>();
        public string Sound { get; set; } = "";
    }
}