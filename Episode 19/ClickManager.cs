using UnityEngine;
using BreakInfinity;

public class ClickManager : MonoBehaviour
{
    private MonsterManager monsterManager;
    public BigDouble damagePerClick = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        monsterManager = GetComponent<MonsterManager>();
    }

    public void DamageClick()
    {
        monsterManager.DamageMonster(damagePerClick);
        StatTracker.instance.damageFromClicks += damagePerClick;
        StatTracker.instance.clicks++;
    }
}
