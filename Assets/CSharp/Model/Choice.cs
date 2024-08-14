using System.Collections.Generic;
using System.Xml.Serialization;

namespace CSharp.Model {
    public class Choice {
        [XmlElement("option")] public List<Option> Options { get; set; }
    }

    public class Option {
        [XmlAttribute("next")] public string Next { get; set; }

        [XmlText] public string Text { get; set; }
    }
}
