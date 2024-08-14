using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TitleController : MonoBehaviour {
    public Text text;

    void Start() { text = GetComponent<Text>(); }

    void Dialogue(string[] arr) {
        string title = arr[0];
        string content = arr[1];
        text.text = "";

        if (!(title == "【旁白】" || title == "【BOSS】")) {
            text.text = title;
        }

        if (title == "【通知】") {
            text.DOColor(Color.yellow, 1);
        }
        else if (title == "【成就】" || title == "【BOSS 出現】") {
            text.DOColor(Color.red, 0);
        }
        else {
            text.DOColor(Color.white, 0);
        }
    }
}
