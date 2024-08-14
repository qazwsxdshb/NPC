using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;

public class Save : MonoBehaviour {
    void Dialogue(string[] arr) {
        string title = arr[0];
        string content = arr[1];

        if (title == "【通知】") {
            TextAsset xmlFile = Resources.Load<TextAsset>("Save");
            XmlDocument document = new XmlDocument();
            document.LoadXml(xmlFile.text); // Xml讀取文件的方法;

            XmlNode node = document.SelectSingleNode("root");
            XmlElement item = document.CreateElement("Item"); //創建新的子節點
            item.SetAttribute("name", content); //創建新子節點屬性名和屬性值
            node.AppendChild(item); //將子節點按照創建順序，添加到xml
            document.AppendChild(node);
        }
    }
}
