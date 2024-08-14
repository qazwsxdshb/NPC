#nullable enable
using System.Xml.Serialization;

namespace CSharp.Model {
    public class Dialogue {
        [XmlAttribute("name")] public string Name { get; set; }

        [XmlAttribute("pic")] public string? Picture { get; set; }

        [XmlText] public string Text { get; set; }
    }
}
