using System.Collections.Generic;
using System.Xml.Serialization;

namespace Source.Configuration
{
    public class Action
    {
        [XmlAttribute]
        public string Command { get; set; }
        [XmlAttribute]
        public string NextStep { get; set; }
        [XmlAttribute]
        public string Sound { get; set; }
        [XmlElement("Text")]
        public List<TextBlock> Text { get; set; }

        public Action()
        {
            Command = "";
            NextStep = "";
            Sound = "";
            Text = new List<TextBlock>();
        }
    }
}