#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using CSharp.Controller;

namespace CSharp.Model {
    public class Drama {
        public Background? SceneBackground;
        public Dialogue? CurrentDialogue;
        public Choice? Choices;
        public GameState GameState = GameState.Start;

        // Assuming newScenes is now part of Drama or passed in some way
        private List<Node> newScenes;
        private int currentDialogueIdx = 0;
        private Node currentScene;

        public Drama(List<Node> newScenes) {
            this.newScenes = newScenes;
            currentScene = newScenes.FirstOrDefault();
            SceneBackground = currentScene?.Background;
            CurrentDialogue = currentScene?.Dialogues.FirstOrDefault();
            Choices = null;
        }

        public void NextScene(string next) {
            currentScene = newScenes.FirstOrDefault(s => s.Id == next);


            if (currentScene == null) {
                GameState = GameState.Ended;
                Choices = newScenes.FirstOrDefault(s => s.Id == "GameEndScene").Choices;
            } else {
                if (currentScene.Achievement != null) {
                    // Unlock achievement
                    var ac = AchievementController.Instance;
                    ac.SaveAchievement(currentScene.Achievement);
                }

                foreach (var currentSceneDialogue in currentScene.Dialogues) {
                    // Remove escape characters and extra spaces
                    var escapeChars = new[] { '\n', '\t', '\v', '\r' };
                    currentSceneDialogue.Text = currentSceneDialogue.Text.Trim(escapeChars)
                                                                    .Replace("  ", "")       // avoid IDE refactor
                                                                    .Replace(" ", "\u00A0"); // non-breaking space
                }

                SceneBackground = currentScene.Background;
                CurrentDialogue = currentScene.Dialogues.FirstOrDefault();
                Choices = null; // Reset choices
            }
        }

        public void NextDrama() {
            currentDialogueIdx++;
            if (currentDialogueIdx < currentScene.Dialogues.Count) {
                CurrentDialogue = currentScene.Dialogues[currentDialogueIdx];
                // Check if the current dialogue is the last one
                if (currentDialogueIdx == currentScene.Dialogues.Count - 1) {
                    // If it's the last dialogue, load Choices
                    Choices = currentScene.Choices;
                } else {
                    // If it's not the last dialogue, set Choices to null
                    Choices = null;
                }
            } else {
                // Adjust here to add features (like function that take next node to go to),
                // Current implementation is just assign to GameEndScene in NextDrama()

                // If there are no more dialogues, move to the next scene
                var nextSceneId = currentScene.Next;
                NextScene(nextSceneId);
                currentDialogueIdx = 0;
            }
        }

        public void MakeChoice(string choice) {
            var selectedOption = currentScene.Choices?
                                             .Options
                                             .FirstOrDefault(option => option.Next == choice);
            if (selectedOption != null) {
                NextScene(selectedOption.Next);
                Choices = currentDialogueIdx == currentScene.Dialogues.Count - 1
                    ? currentScene.Choices
                    : null;
                currentDialogueIdx = 0; // Reset dialogue index for new scene
            }
        }
    }
}
