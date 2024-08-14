using System.Collections.Generic;
using UnityEngine;

namespace CSharp.Controller {
    public class AchievementController {
        private static AchievementController instance;

        // private List<string> achievement = new();
        public static AchievementController Instance {
            get { return instance ??= new AchievementController(); }
        }

        public void DropData() { PlayerPrefs.DeleteAll(); }

        public string GetID() {
            var id = PlayerPrefs.GetString("ID");
            if (id == "") {
                // Generate a unique ID for the player, like: Fedi4s
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                for (int i = 0; i < 6; i++) {
                    id += chars[Random.Range(0, chars.Length - 1)];
                }
            }

            SaveID(id);
            return id;
        }

        private void SaveID(string id) {
            PlayerPrefs.SetString("ID", id);
            PlayerPrefs.Save();
        }

        public List<string> GetAchievement() {
            List<string> achievement = new();

            var achievementString = PlayerPrefs.GetString("achievement");
            var deserializeAchievements =
                AchievementJsonHelper.DeserializeAchievements(achievementString);
            achievement.AddRange(deserializeAchievements ?? new List<string>());
            return achievement;
        }

        public void SaveAchievement(string newAchievement) {
            List<string> existingAchievement = GetAchievement();
            if (!existingAchievement.Contains(newAchievement)) {
                existingAchievement.Add(newAchievement);
                var finalAchievements = AchievementJsonHelper.SerializeAchievements(existingAchievement);
                PlayerPrefs.SetString("achievement", finalAchievements);
                PlayerPrefs.Save();
            }
        }
    }

    static class AchievementJsonHelper {
        [System.Serializable]
        public class AchievementListWrapper {
            public List<string> achievements;
        }

        public static string SerializeAchievements(List<string> achievementData) {
            AchievementListWrapper wrapper = new AchievementListWrapper { achievements = achievementData };
            return JsonUtility.ToJson(wrapper);
        }

        public static List<string> DeserializeAchievements(string json) {
            AchievementListWrapper wrapper = JsonUtility.FromJson<AchievementListWrapper>(json);
            return wrapper?.achievements ?? new List<string>();
        }
    }
}
