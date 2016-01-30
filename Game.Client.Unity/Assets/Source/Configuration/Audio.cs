using System.Xml.Serialization;

namespace Source.Configuration
{
    public class Audio : IConfiguration
    {
        [XmlAttribute]
        public string Type { get; set; }
        [XmlAttribute]
        public string Key { get; set; }
        public string File { get; set; }

        public Audio()
        {
            Type = "";
            Key = "";
            File = "";
        }
    }
}
