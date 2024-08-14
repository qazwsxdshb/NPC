using System;
using System.Collections.Generic;
using System.Linq;
using CSharp.Controller;
using CSharp.Enu;
using CSharp.State;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace CSharp.Scene {
    public class StartScene : MonoBehaviour {
        private DramaController dc;

        // Already assign in inspector
        public Image mainSceneBackground;
        public List<Button> buttons;
        public List<Text> buttonTexts;
        public Text tipsText;

        // private static Dictionary<StartSceneEnu, Button> buttonDict = new Dictionary<StartSceneEnu, Button>();


        private void InitializeUIComponents() {
            buttons.AddRange(GetComponentsInChildren<Button>());
            buttonTexts.AddRange(GetComponentsInChildren<Text>());

            buttons = buttons.Distinct().ToList();
            buttonTexts = buttonTexts.Distinct().ToList();

            foreach (var button in buttons) {
                try {
                    StartSceneEnu type = (StartSceneEnu)Enum.Parse(typeof(StartSceneEnu), button.name);
                    // buttonDict.Add(type, button);
                    AssignButtonListener(type, button);
                } catch (Exception e) {
                    Debug.LogWarning(e);
                }
            }
        }

        private void Awake() { dc = DramaController.Instance; }

        private void Start() {
            // DOTween.Init();

            InitializeUIComponents();

            SetButtonsInteractable(false);
            SetButtonsInteractable(
                new List<StartSceneEnu> {
                    StartSceneEnu.StartButton,
                    StartSceneEnu.AchievementButton,
                    StartSceneEnu.GitHubButton
                }, true);
        }

        void OnApplicationQuit() { PlayerPrefs.DeleteAll(); }

        void SetButtonsInteractable(List<StartSceneEnu> btns, bool newState) {
            if (buttons.Count == 0) {
                return;
            }

            foreach (var btn in btns) {
                Debug.Log("Now: " + btn);
                buttons
                    .Find(x => x.name == btn.ToString())
                    .interactable = newState;
            }
        }

        void SetButtonsInteractable(bool newState) {
            foreach (var button in buttons) {
                button.interactable = newState;
            }
        }

        private void AssignButtonListener(StartSceneEnu type, Button button) {
            switch (type) {
                case StartSceneEnu.StartButton:
                case StartSceneEnu.ReturnButton:
                    button.onClick.AddListener(() => FadeSettings(type));
                    break;
                case StartSceneEnu.NormalButton:
                case StartSceneEnu.HardButton:
                    button.onClick.AddListener(() => OnDifficultySelected(type));
                    break;
                case StartSceneEnu.AchievementButton:
                    button.onClick.AddListener(() => {
                        Util.Scene.SwitchScene(GameScene.AchievementScene);
                    });
                    break;
                case StartSceneEnu.GitHubButton:
                    button.onClick.AddListener(Util.Scene.OpenGithubPage);
                    break;
            }
        }

        void OnDifficultySelected(StartSceneEnu difficulty) {
            switch (difficulty) {
                case StartSceneEnu.NormalButton:
                    dc.Initialize(DramaPaths.Normal);
                    break;
                case StartSceneEnu.HardButton:
                    dc.Initialize(DramaPaths.Hard);
                    break;
            }

            Util.Scene.SwitchScene(GameScene.LoadingScene);
        }

        void Fade(List<Enum> items, float endAlpha, float duration) {
            foreach (var item in items) {
                if (item is StartSceneEnu) {
                    buttons
                        .Find(x => x != null && x.name == item.ToString())
                        .image
                        .DOFade(endAlpha, duration);
                } else if (item is StartSceneBtnText) {
                    buttonTexts
                        .Find(x => x != null && x.name == item.ToString())
                        .DOFade(endAlpha, duration);
                } else {
                    Debug.LogError("Invalid enum type");
                }
            }
        }

        private void FadeSettings(StartSceneEnu setting) {
            var fadeOutItems = new List<Enum>();
            var fadeInItems = new List<Enum>();
            var interactableButtons = new List<StartSceneEnu>();
            float backgroundFadeTo = 1;

            switch (setting) {
                case StartSceneEnu.StartButton:
                    fadeOutItems.AddRange(new Enum[] {
                        StartSceneEnu.StartButton, StartSceneEnu.AchievementButton,
                        StartSceneBtnText.StartText, StartSceneBtnText.AchievementText
                    });
                    fadeInItems.AddRange(new Enum[] {
                        StartSceneEnu.HardButton, StartSceneEnu.NormalButton,
                        StartSceneBtnText.HardText, StartSceneBtnText.NormalText
                    });
                    interactableButtons.AddRange(new StartSceneEnu[]
                        { StartSceneEnu.NormalButton, StartSceneEnu.HardButton, StartSceneEnu.ReturnButton });
                    backgroundFadeTo = 0.2F;
                    break;
                case StartSceneEnu.ReturnButton:
                    fadeOutItems.AddRange(new Enum[] {
                        StartSceneEnu.NormalButton, StartSceneEnu.HardButton,
                        StartSceneBtnText.NormalText, StartSceneBtnText.HardText
                    });
                    fadeInItems.AddRange(new Enum[] {
                        StartSceneEnu.StartButton, StartSceneEnu.AchievementButton, StartSceneBtnText.StartText,
                        StartSceneBtnText.AchievementText
                    });
                    interactableButtons.AddRange(new StartSceneEnu[]
                        { StartSceneEnu.StartButton, StartSceneEnu.AchievementButton, StartSceneEnu.GitHubButton });
                    backgroundFadeTo = 1;
                    break;
            }

            Fade(fadeOutItems, 0, 0.1f); // Adjust duration as needed
            Fade(fadeInItems, 1, 0.1f);  // Adjust duration as needed
            SetButtonsInteractable(false);
            SetButtonsInteractable(interactableButtons, true);
            mainSceneBackground.DOFade(backgroundFadeTo, 0.3F); // Adjust duration as needed
        }
    }
}
