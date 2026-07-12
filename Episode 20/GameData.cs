using BreakInfinity;
using System;

[Serializable]
public class GameData
{
    public BigDouble money;
    public BigDouble moneyPerSec;
    public BigDouble prestigeCurrency;
    public DateTime saveTime;

    #region Counts
    public BigDouble clickUpgradeCount;
    public BigDouble idleUpgradeCount;
    public BigDouble generatorCount;
    #endregion

    #region Stats
    public BigDouble timePlayed;
    public BigDouble totalDamage;
    public BigDouble damageFromClicks;
    public BigDouble damageFromIdle;
    public BigDouble clicks;
    #endregion

    #region Monster
    public int currentMonsterIndex;
    public BigDouble currentMonsterHealth;
    #endregion

    #region Stage
    public BigDouble stageNumber;
    public int killCount;
    public int currentStageIndex;
    #endregion

}
