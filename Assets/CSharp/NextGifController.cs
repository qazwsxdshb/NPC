using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextGifController : MonoBehaviour {
    public GameObject Gif;

    private bool B = false;

    IEnumerator NextGif() {
        Gif.GetComponent<Image>().sprite = Resources.Load<Sprite>("NextGif/1");
        yield return new WaitForSeconds(0.3f);
        Gif.GetComponent<Image>().sprite = Resources.Load<Sprite>("NextGif/2");
        yield return new WaitForSeconds(0.3f);
        keep();
    }

    void keep() {
        if (B) {
            StartCoroutine("NextGif");
        }
    }

    void NextStandby() {
        B = true;
        StartCoroutine("NextGif");
    }

    void GetPressed(string obj) { B = false; }
}
