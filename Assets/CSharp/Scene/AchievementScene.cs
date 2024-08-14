using System.Collections.Generic;
using CSharp.Controller;
using CSharp.State;
using UnityEngine;
using UnityEngine.UI;

namespace CSharp.Scene {
    public class AchievementScene : MonoBehaviour {
        public List<Text> texts;
        public Text idText;
        public Button returnButton;
        public Button producerButton;


        void Start() {
            AchievementController ac = AchievementController.Instance;
            var achievement = ac.GetAchievement();
            var playerID = ac.GetID();
            InitializeUIComponent(achievement, playerID);
        }

        private void InitializeUIComponent(List<string> achievement, string playerID) {
            for (int i = 0; i < achievement.Count; i++) {
                texts[i].enabled = true;
                texts[i].text = achievement[i];
            }

            for (int i = achievement.Count; i < 10; i++) {
                texts[i].enabled = false;
            }

            idText.text = "ID: " + playerID;

            returnButton.onClick.AddListener(() => Util.Scene.SwitchScene(GameScene.StartScene));
            producerButton.onClick.AddListener(() => Util.Scene.SwitchScene(GameScene.ProducerScene));
        }
    }
}
