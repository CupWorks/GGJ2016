using System.Collections.Generic;
using System.Xml.Serialization;

namespace Game.Core.Configuration
{
    public class StoryStep : IConfigurationList
    {
        [XmlAttribute]
        public string Key { get; set; } = "";
        [XmlAttribute]
        public string NextStep { get; set; } = "";
		[XmlAttribute]
		public string HelpText { get; set; } = "";
        [XmlAttribute]
        public string Sound { get; set; } = "";
        [XmlAttribute]
        public bool Mute { get; set; } = false;
        [XmlElement("Text")]
        public List<TextBlock> Text { get; set; } = new List<TextBlock>();
        [XmlElement("Action")]
        public List<Action> ActionList { get; set; } = new List<Action>();
    }
}