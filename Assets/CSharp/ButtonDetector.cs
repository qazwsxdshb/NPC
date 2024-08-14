using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonDetector : MonoBehaviour
{
    public Button button1;
    public Button button2;
    public Button button3;
    public Button button4;
    private Text buttonText1;
    private Text buttonText2;
    private Text buttonText3;
    private Text buttonText4;

    private bool chooseItem = false;

    // Start is called before the first frame update
    void Start()
    {
        buttonText1 = button1.GetComponentInChildren<Text>();
        buttonText2 = button2.GetComponentInChildren<Text>();
        buttonText3 = button3.GetComponentInChildren<Text>();
        buttonText4 = button4.GetComponentInChildren<Text>();

        button1.interactable = false;
        button2.interactable = false;
        button3.interactable = false;
        button4.interactable = false;
        buttonText1.text = "";
        buttonText2.text = "";
        buttonText3.text = "";
        buttonText4.text = "";

        chooseItem = false;
    }

    void Option1(string[] arr)
    {
        string title = arr[0];
        button1.interactable = true;
        buttonText1.text = title;
    }
    void Option2(string[] arr)
    {
        string title = arr[0];
        button2.interactable = true;
        buttonText2.text = title;
    }
    void Option3(string[] arr)
    {
        string title = arr[0];
        button3.interactable = true;
        buttonText3.text = title;
    }
    void Option4(string[] arr)
    {
        string title = arr[0];
        button4.interactable = true;
        buttonText4.text = title;
    }
    void ChooseItem()
    {
        chooseItem = true;
    }
    void ChooseOption()
    {
        chooseItem = false;
    }
    void CleanButton()
    {
        buttonText1.text = "";
        buttonText2.text = "";
        buttonText3.text = "";
        buttonText4.text = "";
        button1.interactable = false;
        button2.interactable = false;
        button3.interactable = false;
        button4.interactable = false;
    }
    public void PressOption1()
    {
        if (chooseItem)
        {
            SendMessage("ChangePath", "/Option[@name='" + buttonText1.text + "']");
        }
        else
        {
            SendMessage("ChangePath", "/Option1");
        }
        SendMessage("CleanButton");
    }

    public void PressOption2()
    {
        if (chooseItem)
        {
            SendMessage("ChangePath", "/Option[@name='" + buttonText2.text + "']");
        }
        else
        {
            SendMessage("ChangePath", "/Option2");
        }
        SendMessage("CleanButton");
    }

    public void PressOption3()
    {
        if (chooseItem)
        {
            SendMessage("ChangePath", "/Option[@name='" + buttonText3.text + "']");
        }
        else
        {
            SendMessage("ChangePath", "/Option3");
        }
        SendMessage("CleanButton");
    }
    public void PressOption4()
    {
        if (chooseItem)
        {
            SendMessage("ChangePath", "/Option[@name='" + buttonText4.text + "']");
        }
        else
        {
            SendMessage("ChangePath", "/Option4");
        }
        SendMessage("CleanButton");
    }
}
