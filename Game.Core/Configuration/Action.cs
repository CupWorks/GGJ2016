using System.Xml.Serialization;

namespace Game.Core.Configuration
{
    public class Action
    {
        [XmlAttribute]
        public string Command { get; set; }
        [XmlAttribute]
        public string NextStep { get; set; }
        public string Text { get; set; }
    }
}