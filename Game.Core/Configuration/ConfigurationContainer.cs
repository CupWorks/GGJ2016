using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System.Linq;

namespace Game.Core.Configuration
{
    public class ConfigurationContainer<TConfiguration> where TConfiguration : IConfiguration
    {
        private Stream DataStream { get; }
        private XmlSerializer XmlSerializer { get; } = new XmlSerializer(typeof(List<TConfiguration>));
        private List<TConfiguration> Collection { get; set; } = new List<TConfiguration>();

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

        public TConfiguration Get(string key)
        {
            return Collection.Single(c => c.Key == key);
        }
    }
}