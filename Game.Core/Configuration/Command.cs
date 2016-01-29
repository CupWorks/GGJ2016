using System.Collections.Generic;
using System.Xml.Serialization;

namespace Game.Core.Configuration
{
    public class Command : IConfiguration
    {
        [XmlAttribute]
        public string Key { get; set; }
        [XmlElement("Word")]
        public List<string> WordList { get; set; } = new List<string>();
    }
}