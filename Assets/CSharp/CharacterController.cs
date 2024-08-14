using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;

public class CharacterController : MonoBehaviour
{
    public Image character1;
    public Image character2;
    void Dialogue(string[] arr)
    {
        string title = arr[0];
        string content = arr[1];
        string picture = arr[2];
        string path = "Character/" + picture;
        if(picture == "clean")
        {
            character2.sprite = Resources.Load<Sprite>("透明");
        }
        if (picture == "cleanAll")
        {
            character1.sprite = Resources.Load<Sprite>("透明");
            character2.sprite = Resources.Load<Sprite>("透明");
        }
        else if (title == "【通知】")
        {
            if (character2.sprite.name != "透明")
            {
                character2.DOColor(Color.grey, 1).SetUpdate(true);
            }
            if (character1.sprite.name != "透明")
            {
                character1.DOColor(Color.white, 1).SetUpdate(true);
            }
        }
        else if (title == "【旁白】" || title == "【小 N】" || title == "【我】")
        {       
            if (!(picture == "" || picture == "clean"))
            {
                character1.sprite = Resources.Load<Sprite>(path);
            }
            if (character1.sprite.name != "透明")
            {
                character1.DOColor(Color.white, 1).SetUpdate(true);
            }
            if (character2.sprite.name != "透明")
            {
                character2.DOColor(Color.grey, 1);
            }
        }
        else
        {
            if (!(picture == "" || picture == "clean"))
            {   
                character2.sprite = Resources.Load<Sprite>(path);
            }
            if (character2.sprite.name != "透明")
            {
                character2.DOColor(Color.white, 1).SetUpdate(true); 
            }
            if (character1.sprite.name != "透明")
            {
                character1.DOColor(Color.grey, 1);
            }
        }
        //else if (title == "【BOSS】")
        //{
        //    if (!(picture == "" || picture == "clean"))
        //    {
        //        character1.sprite = Resources.Load<Sprite>(path);
        //    }
        //    if (character1.sprite.name != "透明")
        //    {
        //        character1.DOColor(Color.white, 1).SetUpdate(true);
        //    }
        //    if (character2.sprite.name != "透明")
        //   {
        //        character2.DOColor(Color.white, 1);
        //   }
        //}
    }
}