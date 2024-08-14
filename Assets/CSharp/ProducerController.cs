using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class ProducerController : MonoBehaviour {
    public void ReturnButtonPress() { SceneManager.LoadScene(1, LoadSceneMode.Single); }
    public void GitHubButtonPress() { Application.OpenURL("https://github.com/itaouo/NPC_Cat_Adventure"); }
}
