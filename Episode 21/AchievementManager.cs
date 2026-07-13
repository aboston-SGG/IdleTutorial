using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class AchievementManager : MonoBehaviour
{
    public List<Achievement> achievements;

    public GameObject achievementPrefab;

    public Transform achievementHolder;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;

    // Update is called once per frame
    void Update()
    {
        CheckAchievements();
    }

    // Check for unlocked achievements
    public void CheckAchievements()
    {
        for(int i = 0; i < achievements.Count; i++)
        {
            var achievement = achievements[i];

            if (achievement.isUnlocked) continue;

            bool shouldUnlock = achievement.achievementType switch
            {
                AchievementTypes.Click => StatTracker.instance.clicks >= achievement.requirementAmount,
                AchievementTypes.Money => StatTracker.instance.moneyEarned >= achievement.requirementAmount,
                AchievementTypes.Time => StatTracker.instance.timePlayed >= achievement.requirementAmount,
                AchievementTypes.Damage => StatTracker.instance.TotalDamage >= achievement.requirementAmount,
                AchievementTypes.MonstersKilled => StatTracker.instance.monstersKilled >= achievement.requirementAmount,
                AchievementTypes.StagesCompleted => StatTracker.instance.stagesCompleted >= achievement.requirementAmount,
                _ => false
            };

            if (shouldUnlock)
            {
                UnlockAchievement(achievement);
                i--;
            }
        }
    }

    // Unlock the achievement and create its UI element
    private void UnlockAchievement(Achievement achievement)
    {
        GameObject newAchievement  = Instantiate(achievementPrefab, achievementHolder);
        AchievementObject achievementObject = newAchievement.GetComponent<AchievementObject>();

        achievementObject.nameText = nameText;
        achievementObject.descriptionText = descriptionText;
        achievementObject.achievement = achievement;

        newAchievement.GetComponent<Image>().sprite = achievement.achievementIcon;

        achievements.Remove(achievement);
    }
}
