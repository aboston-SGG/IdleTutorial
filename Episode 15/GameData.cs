using BreakInfinity;
using System;

[Serializable]
public class GameData
{
    public BigDouble money;
    public BigDouble prestigeCurrency;
    public DateTime saveTime;

    #region Counts
    public BigDouble clickUpgradeCount;
    public BigDouble idleUpgradeCount;
    public BigDouble generatorCount;
    #endregion

    #region Stats
    public BigDouble timePlayed;
    public BigDouble moneyFromClicks;
    public BigDouble moneyFromIdle;
    public BigDouble clicks;
    #endregion

}
