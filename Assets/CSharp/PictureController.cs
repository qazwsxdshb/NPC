using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Globalization;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;

public class PictureController : MonoBehaviour {
    public Image background_Image;
    public Transform background_Transform;
    public Image mask;
    public Image item;
    public Image HP;
    public Image HPBar;
    public Text gameOverText;
    public Transform gameOver_Transform;
    float HPScale = 1F;

    Color customColor = new Color(0.98f, 0.948f, 0.744f, 1.0f);

    List<string> itemList;

    void Start() {
        HPScale = 1F;
        HPBar.fillAmount = HPScale;
        HP.DOFade(0, 0f);
        HPBar.DOFade(0, 0f);
        itemList = new List<string>();
    }

    IEnumerator Background(string[] arr) {
        string picture = arr[2];

        if (arr[1] == "Before End") {
            yield return new WaitForSeconds(0.8f);
            background_Transform.DOScale(background_Transform.localScale * 5, 1f);
            mask.DOFade(1, 1f);
        }
        else if (arr[1] == "End") {
            background_Transform.DOScale(background_Transform.localScale / 5, 0f);
            mask.DOFade(0, 2f);
        }
        else if (arr[1] == "Game Over") {
            gameOverText.DOFade(1, 0f);
            gameOver_Transform.DOMoveY(770, 1f, true);
            gameOver_Transform.DOScale(gameOver_Transform.localScale * 3, 1f);
        }
        else if (arr[1] != "Skip Animation") {
            background_Image.DOColor(Color.black, 0.5f);
            yield return new WaitForSeconds(0.5f);
        }

        if (!(arr[1] == "BeforeEnd" || arr[1] == "End")) {
            background_Image.sprite = Resources.Load<Sprite>("Background/" + picture);
            background_Image.DOColor(Color.white, 0.5f);
        }
        else if (arr[1] == "End") {
            background_Image.sprite = Resources.Load<Sprite>("Background/" + picture);
            background_Image.DOColor(Color.white, 0f);
        }
    }

    void Dialogue(string[] arr) {
        string title = arr[0];
        string content = arr[1];
        string picture = arr[2];

        if (arr[0] == "【通知】" && picture != "") {
            item.sprite = Resources.Load<Sprite>("Item/" + picture);
            if (itemList.Contains(picture)) {
                itemList.Remove(picture);
            }
            else {
                if (picture == "塑膠硬幣") {
                    itemList.Remove("塑膠鐵製硬幣");
                    itemList.Add("鐵製硬幣");
                }
                else if (picture == "鐵製硬幣") {
                    itemList.Remove("塑膠鐵製硬幣");
                    itemList.Add("塑膠硬幣");
                }
                else {
                    itemList.Add(picture);
                }
            }
        }
        else {
            item.sprite = Resources.Load<Sprite>("透明");
        }

        if (title == "【BOSS 出現】") {
            HP.DOFade(1, 0f);
            HPBar.DOFade(1, 0f);
        }
    }

    void ChooseItem(string[] arr) {
        if (HPScale <= 0 || itemList.Count == 0) {
            HP.DOFade(0, 0f);
            HPBar.DOFade(0, 0f);
            arr[0] = arr[1];
            arr[1] = "0";
            SendMessage("ChangePath", arr);
        }
        else {
            for (int i = 1; i <= itemList.Count; i++) {
                if (i == 5) {
                    break;
                }

                arr[0] = itemList[i - 1];
                SendMessage("Option" + i.ToString(), arr);
            }
        }
    }

    void HPminus(string[] arr) {
        HPScale -= Convert.ToSingle(arr[1]);
        HPBar.DOFillAmount(HPScale, 3f);
    }
}
