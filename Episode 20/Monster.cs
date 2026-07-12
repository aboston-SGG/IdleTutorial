using UnityEngine;
using BreakInfinity;

[CreateAssetMenu(fileName = "New Monster", menuName = "Scriptable Objects/Monster")]
public class Monster : ScriptableObject
{
    public Sprite monsterSprite;
    public Sprite damageSprite;
    public string monsterName;
    public BigDouble baseHealth;
    public BigDouble baseReward;
    public float scalingModifier;
}
