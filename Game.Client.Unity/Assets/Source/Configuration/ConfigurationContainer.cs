using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace Source.Configuration
{
    public class ConfigurationContainer<TConfiguration> where TConfiguration : IConfiguration
    {
        private XmlSerializer XmlSerializer { get; set; } 
        private List<TConfiguration> Collection { get; set; }

        public ConfigurationContainer(TextReader textReader)
        {
            XmlSerializer = new XmlSerializer(typeof(List<TConfiguration>));
            Collection = XmlSerializer.Deserialize(textReader) as List<TConfiguration>;
        }

        public TConfiguration Get(string key)
        {
            return Collection.Single(c => c.Key == key);
        }

        public IEnumerable<TConfiguration> Get(Func<TConfiguration, bool> whereFunction)
        {
            return Collection.Where(whereFunction);
        }
    }
}