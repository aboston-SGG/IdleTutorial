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
            switch(achievements[i].achievementType)
            {
                case AchievementTypes.Click:
                    if(StatTracker.instance.clicks >= achievements[i].requirementAmount)
                    {
                        UnlockAchievement(achievements[i]);
                    }
                    break;
                case AchievementTypes.Money:
                    if (StatTracker.instance.TotalMoney >= achievements[i].requirementAmount)
                    {
                        UnlockAchievement(achievements[i]);
                    }
                    break;
                case AchievementTypes.Time:
                    if (StatTracker.instance.timePlayed >= achievements[i].requirementAmount)
                    {
                        UnlockAchievement(achievements[i]);
                    }
                    break;
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
