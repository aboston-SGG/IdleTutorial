using UnityEngine;
using BreakInfinity;

[CreateAssetMenu(fileName = "New Achievement", menuName = "ScriptableObjects/Achievement")]
public class Achievement : ScriptableObject
{
    public string achievementName;
    public string achievementDescription;
    public Sprite achievementIcon;
    public BigDouble requirementAmount;
    public AchievementTypes achievementType;
}

public enum AchievementTypes
{
    Click,
    Money,
    Time
}
