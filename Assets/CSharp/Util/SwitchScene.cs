using System;
using System.ComponentModel;
using CSharp.State;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CSharp.Util {
    public class Scene {
        public static void SwitchScene(GameScene gs, LoadSceneMode mode = LoadSceneMode.Single) {
            if (!Enum.IsDefined(typeof(GameScene), gs))
                throw new InvalidEnumArgumentException(nameof(gs), (int)gs, typeof(GameScene));
            // Scenes are defined in the File/build settings
            SceneManager.LoadScene("Scenes/" + gs, mode);
        }

        public static void OpenGithubPage() { Application.OpenURL("https://github.com/itaouo/NPC_Cat_Adventure"); }
    }
}
