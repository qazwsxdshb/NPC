using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class DifficultyController : MonoBehaviour {
    public Image difficultyBackground;

    // public Button easyButton;
    public Button normalButton;
    public Button hardButton;

    public Button startButton;
    public Button achievementButton;
    public Button returnButton;
    public Button githubButton;

    public Text tipsText;

    // private Text easyButtonText;
    private Text normalButtonText;

    private Text hardButtonText;

    // Start is called before the first frame update
    void Start() {
        // easyButtonText = easyButton.GetComponentInChildren<Text>();
        normalButtonText = normalButton.GetComponentInChildren<Text>();
        hardButtonText = hardButton.GetComponentInChildren<Text>();

        // easyButton.interactable = false;
        normalButton.interactable = false;
        hardButton.interactable = false;
        returnButton.interactable = false;
        difficultyBackground.DOFade(0, 0);
        // easyButtonText.DOFade(0, 0);
        normalButtonText.DOFade(0, 0);
        hardButtonText.DOFade(0, 0);
    }

    public void PressStartButton() {
        // easyButton.interactable = true;
        normalButton.interactable = true;
        hardButton.interactable = true;
        returnButton.interactable = true;
        startButton.interactable = false;
        achievementButton.interactable = false;
        githubButton.interactable = false;

        difficultyBackground.DOFade(0.8f, 0);
        // easyButtonText.DOFade(1, 0);
        normalButtonText.DOFade(1, 0);
        hardButtonText.DOFade(1, 0);
    }

    public void PressAchievementButton() { SceneManager.LoadScene(3, LoadSceneMode.Single); }

    public void PressReturnButton() {
        // easyButton.interactable = false;
        normalButton.interactable = false;
        hardButton.interactable = false;
        returnButton.interactable = false;
        startButton.interactable = true;
        achievementButton.interactable = true;
        difficultyBackground.DOFade(0, 0);
        // easyButtonText.DOFade(0, 0);
        normalButtonText.DOFade(0, 0);
        hardButtonText.DOFade(0, 0);
        normalButtonText.DOFade(0, 0);
    }

    // public void PointEasyButton()
    // {
    //     if(easyButton.interactable == true)
    //     {
    //          tipsText.DOColor(Color.green, 0);
    //          tipsText.text = "簡單！如果你有注意選項的話……";
    //      }
    // }
    public void PointNormalButton() {
        if (normalButton.interactable == true) {
            tipsText.DOColor(Color.yellow, 0);
            tipsText.text = "普普通通，稍微思考一下也能夠通關啦。";
        }
    }

    public void PointHardButton() {
        if (hardButton.interactable == true) {
            tipsText.DOColor(Color.red, 0);
            tipsText.text = "極度困難！非高手勿輕易進入！";
        }
    }

    public void PointOutButton() { tipsText.text = ""; }

    // public void PressEasyButton()
    // {
    //     PlayerPrefs.SetString("Drama", "Drama/Easy");
    //     SendMessage("PressButton");
    // }
    public void PressNormalButton() {
        PlayerPrefs.SetString("Drama", "Drama/Normal");
        SendMessage("PressButton");
    }

    public void PressHardButton() {
        PlayerPrefs.SetString("Drama", "Drama/Hard");
        SendMessage("PressButton");
    }

    void PressButton() { SceneManager.LoadScene(2, LoadSceneMode.Single); }
    public void PressGitHubButton() { Application.OpenURL("https://github.com/itaouo/NPC_Cat_Adventure"); }
}
