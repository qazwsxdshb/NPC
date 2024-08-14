using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class RestartController : MonoBehaviour {
    void Achievement(string[] arr) { PlayerPrefs.SetString("Achievement" + arr[0], "true"); }
    void Home(string[] arr) { SceneManager.LoadScene(1, LoadSceneMode.Single); }

    void Quit(string[] arr) {
        PlayerPrefs.DeleteAll();
        Application.Quit();
    }

    void OnApplicationQuit() { PlayerPrefs.DeleteAll(); }
    void Gameover(string[] arr) { }
    void Restart() { SceneManager.LoadScene(4, LoadSceneMode.Single); }
} //image.DOFade(0.8f, 3f);
