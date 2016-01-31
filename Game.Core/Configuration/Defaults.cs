using System.Collections.Generic;
using System.Xml.Serialization;

namespace Game.Core.Configuration
{
    public class Defaults
    {
        public int InputTries { get; set; }
        [XmlElement("InputWarning")]
        public List<string> InputWarnings { get; set; } = new List<string>();
        public string InstructionText { get; set; } = "";
    }
} 