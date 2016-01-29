using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;

namespace Game.Core.Configuration
{
    public class ConfigurationContainer<TConfiguration>
    {
        private Stream DataStream { get; }
        private XmlSerializer XmlSerializer { get; } = new XmlSerializer(typeof(List<TConfiguration>));

        public List<TConfiguration> Collection { get; set; } = new List<TConfiguration>();

        public ConfigurationContainer(Stream dataStream)
        {
            DataStream = dataStream;
        }

        public void ReadFromStream()
        {
            Collection = XmlSerializer.Deserialize(DataStream) as List<TConfiguration>;
        }

        public void WriteToStream()
        {
            XmlSerializer.Serialize(DataStream, Collection);
        }
    }
}