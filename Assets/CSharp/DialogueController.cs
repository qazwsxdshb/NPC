using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;

public class DialogueController : MonoBehaviour
{   
    public GameObject Text1;
    public GameObject Text2;
    private Text text1;
    private Text text2;
    void Start()
    {
        text1 = Text1.GetComponent<Text>();
        text2 = Text2.GetComponent<Text>();

        text1.text = "";
        text2.text = "";
    }
    IEnumerator Dialogue(string[] arr)
    {   
        string title = arr[0];
        string content = arr[1];
        string picture = arr[2];

        text1.text = "";
        text2.text = "";
        float speed = content.Length * 0.01F; ///////
        if (content.Contains(" "))
        {
            content = content.Replace(" ", "\u00A0");
        }
        if (title == "【旁白】" || title == "【BOSS】")
        {
            text1.text = "";
            text2.DOText(content, speed).SetUpdate(true);
        }
        else
        {
            text2.text = "";
            text1.DOText(content, speed).SetUpdate(true);
        }
        
        yield return new WaitForSeconds(speed+0.1f);

        SendMessageUpwards("NextStandby");
        BroadcastMessage("NextStandby");
        
    }
}
