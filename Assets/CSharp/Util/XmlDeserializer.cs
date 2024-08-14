using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using CSharp.Controller;
using CSharp.Model;
using UnityEngine;

namespace CSharp.Util {
    public class XmlDeserializer {
        public static List<Node> LoadDrama(DramaPath dp) {
            TextAsset xmlFile = Resources.Load<TextAsset>(dp.Path);
            if (xmlFile == null) {
                Debug.LogError("Failed to load XML file from Resources.");
                return null;
            }

            using TextReader reader = new StringReader(xmlFile.text);
            XmlSerializer serializer = new XmlSerializer(typeof(SceneNode));

            var drama = (SceneNode)serializer.Deserialize(reader);
            reader.Close();

            List<Node> nodes = drama?.Nodes ?? new List<Node>();
            return nodes;
        }
    }
}
