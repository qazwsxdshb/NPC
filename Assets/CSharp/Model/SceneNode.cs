#nullable enable
using System.Collections.Generic;
using System.Xml.Serialization;
using JetBrains.Annotations;

namespace CSharp.Model {
    [XmlRoot("SceneNode")]
    public class SceneNode {
        [XmlElement("node")] public List<Node> Nodes { get; set; }
    }

    public class Node {
        [XmlAttribute("idText")] public string Id { get; set; }

        [XmlElement("background")] public Background? Background { get; set; }

        [XmlElement("dialogue")] public List<Dialogue> Dialogues { get; set; }

        [XmlElement("choice")] public Choice? Choices { get; set; }

        [XmlElement("next")] public string? Next { get; set; }
        
        [XmlElement("achievement")] public string? Achievement { get; set; }
    }
    
    public class Background {
        [XmlAttribute("pic")] public string Picture { get; set; }
    }
}
