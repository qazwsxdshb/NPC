using CSharp.State;
using UnityEngine;
using UnityEngine.UI;

namespace CSharp.Scene {
    public class ProducerScene : MonoBehaviour {
        public Button returnButton;
        public Button gitHubButton;

        void Start() {
            returnButton.onClick.AddListener(() => Util.Scene.SwitchScene(GameScene.AchievementScene));
            gitHubButton.onClick.AddListener(() => Util.Scene.OpenGithubPage());
        }
    }
}
