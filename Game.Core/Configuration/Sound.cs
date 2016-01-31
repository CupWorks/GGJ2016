using System.Xml.Serialization;

namespace Game.Core.Configuration
{
    public class Sound : IConfigurationList
    {
        [XmlAttribute]
        public string Key { get; set; } = "";
        public string File { get; set; } = "";
    }
}
