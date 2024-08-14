using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using DG.Tweening;
public class AchieveController : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject TextParent;
    public Text IDText;
    
    private Text[] texts;
    private string achi;
    private string id;

    void Start()
    {
        // IDText = GetComponent("IDText") as Text;
        IDText.text = "ID: " + PlayerPrefs.GetString("ID", "false");
        Text[] texts = TextParent.GetComponentsInChildren<Text>();
        //texts[0].text = "  【簡單版】 結局一";
        //texts[1].text = "  【簡單版】 結局二";
        //texts[2].text = "  【簡單版】 結局三";
        //texts[0].DOColor(Color.green, 0);
        //texts[1].DOColor(Color.green, 0);
        //texts[2].DOColor(Color.green, 0);
        texts[0].text = "  【普通版】 結局一";
        texts[1].text = "  【普通版】 結局二";
        texts[2].text = "  【普通版】 結局三";
        texts[0].DOColor(Color.yellow, 0);
        texts[1].DOColor(Color.yellow, 0);
        texts[2].DOColor(Color.yellow, 0);
        texts[3].text = "  【極度困難版】 結局一";
        texts[4].text = "  【極度困難版】 結局二";
        texts[5].text = "  【極度困難版】 結局三";
        texts[3].DOColor(Color.red, 0);
        texts[4].DOColor(Color.red, 0);
        texts[5].DOColor(Color.red, 0);
        foreach (Text t in texts)
        {
            t.DOFade(0, 0);
        }
        for (int i = 0; i < 10; i++)
        {
            achi = PlayerPrefs.GetString("Achievement" + Convert.ToString(i), "false");
            if(achi == "true")
            {
                texts[i].DOFade(1, 0);
            }
        }
    }
    void Achievement(string[] arr)
    {
        //texts[Int32.Parse(arr[0])].DOFade(1, 0);
    }

    public void ReturnButtonPress()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
    public void ProducerButtonPress()
    {
        SceneManager.LoadScene(5, LoadSceneMode.Single);
    }
}
