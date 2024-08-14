#nullable enable
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CSharp.Controller;
using CSharp.Enu;
using CSharp.Model;
using CSharp.State;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace CSharp.Scene {
    public class MainScene : MonoBehaviour {
        public Image mainSceneBackground;
        public List<Text> dialogueTexts;
        public Image dialogueItemImage;
        public Image dialogueAnimation;
        public Image hp;
        public List<Image> characters;
        public Image restartImage;
        public Button dialogueBox;
        public List<Button> choices;

        private static Dictionary<Choices, Button> choiceDict;
        private static Dictionary<DialogueBox, Text> dialogueDict;
        private static Dictionary<Character, Image> characterDict;

        private bool isLooping = false;

        public void StartLoopingImage() {
            if (!isLooping) {
                isLooping = true;
                StartCoroutine(LoopImage());
            }
        }

        public void StopLoopingImage() { isLooping = false; }

        IEnumerator LoopImage() {
            while (isLooping) {
                dialogueAnimation.sprite = Resources.Load<Sprite>("NextGif/1");
                yield return new WaitForSeconds(0.3f); // Wait for 0.3 seconds
                if (!isLooping) break;                 // Check if looping was stopped during the wait
                dialogueAnimation.sprite = Resources.Load<Sprite>("NextGif/2");
                yield return new WaitForSeconds(0.3f); // Wait for another 0.3 seconds
            }
        }

        private void InitializeUIComponents() {
            // Populate UI components from children
            dialogueTexts.AddRange(GetComponentsInChildren<Text>(true));
            characters.AddRange(GetComponentsInChildren<Image>(true)
                .Where(x => x.name == "NpcImage" || x.name == "PlayerImage"));
            choices.AddRange(GetComponentsInChildren<Button>(true));

            dialogueDict = new Dictionary<DialogueBox, Text>();
            characterDict = new Dictionary<Character, Image>();
            choiceDict = new Dictionary<Choices, Button>();

            restartImage.enabled = false;

            dialogueBox.onClick.AddListener(() => {
                var dc = DramaController.Instance;
                dc.NextDrama();
                UpdateDialogueBoxState(dc.drama);
            });

            foreach (var dialogueText in dialogueTexts) {
                if (Enum.TryParse(dialogueText.name, out DialogueBox type)) {
                    dialogueDict[type] = dialogueText;
                } else {
                    Debug.LogWarning($"Unrecognized DialogueBox name: {dialogueText.name}");
                }
            }

            foreach (var choice in choices) {
                if (Enum.TryParse(choice.name, out Choices type)) {
                    choiceDict[type] = choice;
                    choice.onClick.AddListener(() => OnChoicesPressed(choice.gameObject));
                } else {
                    Debug.LogWarning($"Unrecognized Choices name: {choice.name}");
                }
            }

            foreach (var character in characters) {
                if (Enum.TryParse(character.name, out Character type)) {
                    characterDict[type] = character;
                } else {
                    Debug.LogWarning($"Unrecognized Character name: {character.name}");
                }
            }
        }

        private void UpdateByDrama(Drama drama) {
            UpdateDialogueText(drama);
            UpdateBackgroundImage(drama);
            UpdateDialogueImage(drama);
            UpdateCharacterImage(drama);
            UpdateDialogueBoxState(drama);
            UpdateChoiceButtons(drama);
        }

        private void UpdateDialogueBoxState(Drama drama) => dialogueBox.interactable = drama.Choices == null;

        private void UpdateDialogueText(Drama drama) {
            var title = drama.CurrentDialogue?.Name ?? string.Empty;
            dialogueDict[DialogueBox.Title].DOText(title, title.Length * 0.01f).SetUpdate(true);

            var content = drama.CurrentDialogue?.Text ?? string.Empty;
            dialogueDict[DialogueBox.Dialogue].text = string.Empty;
            dialogueDict[DialogueBox.Dialogue].DOText(content, content.Length * 0.01f).SetUpdate(true);
        }

        // has to modify dialogue xml: separate Character image and Item image
        private void UpdateDialogueImage(Drama drama) {
            var dialogueSprite = Resources.Load<Sprite>($"Item/{drama.CurrentDialogue?.Picture}");
            if (dialogueSprite != null) {
                dialogueItemImage.enabled = true;
                dialogueItemImage.sprite = dialogueSprite;
            } else {
                dialogueItemImage.enabled = false;
            }
        }

        private void UpdateBackgroundImage(Drama drama) {
            var sceneBackground = drama.SceneBackground?.Picture;
            if (sceneBackground != null) {
                mainSceneBackground.sprite = Resources.Load<Sprite>($"BackGround/{drama.SceneBackground.Picture}");
            }
        }

        private void UpdateChoiceButtons(Drama drama) {
            var totalOptions = drama.Choices?.Options.Count ?? 0;
            if (totalOptions == 0) {
                StartLoopingImage();
            } else {
                StopLoopingImage();
            }

            for (int i = 0; i < totalOptions; i++) {
                var choiceButton = choiceDict[(Choices)i];
                choiceButton.enabled = choiceButton.interactable = true;

                var choiceImage = choiceButton.GetComponentInChildren<Image>();
                choiceImage.enabled = true;

                var choiceText = choiceButton.GetComponentInChildren<Text>();
                choiceText.enabled = true;
                choiceText.text = drama.Choices.Options[i].Text;
            }

            foreach (var choice in choiceDict.Values.Skip(totalOptions)) {
                choice.enabled = choice.interactable = false;

                var choiceImage = choice.GetComponentInChildren<Image>();
                choiceImage.enabled = false;

                var choiceText = choice.GetComponentInChildren<Text>();
                choiceText.enabled = false;
            }
        }

        private void OnChoicesPressed(GameObject go) {
            // Attempt to parse the GameObject's name to the Choices enum
            DramaController dc = DramaController.Instance;
            if (Enum.TryParse(go.name, out Choices selectedChoice)) {
                // Find the corresponding option in the drama's choices
                var option = dc.drama?
                               .Choices?
                               .Options
                               .FirstOrDefault(x => x.Text == go.GetComponentInChildren<Text>().text);

                if (option != null) {
                    Debug.Log($"{option.Text}-{option.Next}: {selectedChoice} Selected.");

                    OnGameStateSelection(option, dc);
                    dc.MakeChoice(option.Next);
                } else {
                    Debug.LogWarning($"No option found for choice: {selectedChoice}");
                }
            } else {
                Debug.LogError($"Failed to parse choice from GameObject name: {go.name}");
            }
        }

        private static void OnGameStateSelection(Option option, DramaController dc) {
            switch (option.Next) {
                case "Home":
                    Util.Scene.SwitchScene(GameScene.StartScene);
                    break;
                case "Restart":
                    dc.Restart();
                    Util.Scene.SwitchScene(GameScene.MainScene);
                    break;
                case "Quit":
                case "ConfirmQuit":
                case "QuitConfirm":
                    Debug.LogWarning("Application Quitting...");
                    var ac = AchievementController.Instance;
                    ac.DropData();
                    Application.Quit();
                    break;
            }
        }

        private void OnSceneChangedHandler(object sender, DramaEventArgs e) {
            // Update UI or perform actions based on the new drama
            UpdateByScene(e.Drama);
        }

        private void UpdateByScene(Drama d) {
            // Update the background image
            UpdateDialogueBoxState(d);
            UpdateBackgroundImage(d);

            // Update dialogue text and image if available
            if (d.CurrentDialogue != null) {
                UpdateDialogueText(d);
                UpdateDialogueImage(d);
            }

            // Update characters (consider remove Npc or Player)
            // because all the character are 1920x1080 picture
            UpdateCharacterImage(d);

            // Update choices
            UpdateChoiceButtons(d);
        }

        private void UpdateCharacterImage(Drama drama) {
            var characterSprite = Resources.Load<Sprite>($"Character/{drama.CurrentDialogue?.Picture}") ??
                                  Resources.Load<Sprite>("透明");
            characterDict[Character.NpcImage].sprite = characterSprite;
        }


        private void OnDramaChangedHandler(object sender, DramaEventArgs d) {
            // Update UI or perform actions based on the new drama
            UpdateByDrama(d.Drama);
        }
        //
        // private void HandleGameState(Drama drama) {
        //     GameState gameState = drama.GameState;
        //
        //     switch (gameState) {
        //         case GameState.Start:
        //             // Handle start state, e.g., show start screen
        //             break;
        //         case GameState.InSelection:
        //             // Handle in-progress state, e.g., enable UI interactions
        //             break;
        //         case GameState.Ended:
        //             // Handle ended state, e.g., show end screen, disable interactions
        //             // ShowEndScreen();
        //             break;
        //         case GameState.GameOver:
        //             break;
        //         case GameState.Restart:
        //             break;
        //     }
        // }


        private void Start() {
            DramaController dc = DramaController.Instance;

            InitializeUIComponents();
            UpdateByDrama(dc.drama);
            StartLoopingImage();
        }

        private void Awake() {
            DramaController dc = DramaController.Instance;
            dc.OnDramaChanged += OnDramaChangedHandler;
            dc.OnSceneChanged += OnSceneChangedHandler;
        }

        private void OnDestroy() {
            DramaController dc = DramaController.Instance;

            if (dc != null) {
                dc.OnDramaChanged -= OnDramaChangedHandler;
                dc.OnSceneChanged -= OnSceneChangedHandler;
            }
        }
    }
}
