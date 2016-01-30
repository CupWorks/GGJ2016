using System.Collections.Generic;
using System.Xml.Serialization;

namespace Source.Configuration
{
    public class StoryStep : IConfiguration
    {
        [XmlAttribute]
        public string Key { get; set; }
        [XmlAttribute]
        public string NextStep { get; set; }
        [XmlElement("Text")]
        public List<TextBlock> Text { get; set; }
        [XmlElement("Action")]
        public List<Action> ActionList { get; set; }

        public StoryStep()
        {
            Key = "";
            NextStep = "";
            Text = new List<TextBlock>();
            ActionList = new List<Action>();
        }
    }
}