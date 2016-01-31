using System.IO;
using System.Xml.Serialization;

namespace Game.Core.Configuration
{
    public class ConfigurationContainer<TConfiguration> where TConfiguration : class
    {
        private Stream DataStream { get; }
        private XmlSerializer XmlSerializer { get; } = new XmlSerializer(typeof(TConfiguration));
        public TConfiguration Object { get; set; }

        public ConfigurationContainer(Stream dataStream)
        {
            DataStream = dataStream;
        }

        public void ReadFromStream()
        {
            Object = XmlSerializer.Deserialize(DataStream) as TConfiguration;
        }

        public void WriteToStream()
        {
            XmlSerializer.Serialize(DataStream, Object);
        }
    }
}