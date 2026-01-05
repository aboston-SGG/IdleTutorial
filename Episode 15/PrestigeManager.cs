using UnityEngine;
using BreakInfinity;

public class PrestigeManager : MonoBehaviour
{
    public static PrestigeManager instance;

    public BigDouble prestigeCurrency;
    public BigDouble prestigeThreshold = 1e6;

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Calculate how much prestige currency the player would gain based on total earned money
    public BigDouble CalculatePrestigeGain(BigDouble totalEarned)
    {
        if (totalEarned < prestigeThreshold) return BigDouble.Zero;

        BigDouble gain = BigDouble.Sqrt(totalEarned / prestigeThreshold);
        return BigDouble.Floor(gain);
    }

    // Check if the player can prestige based on total earned money
    public bool CanPrestige(BigDouble totalEarned)
    {
        return CalculatePrestigeGain(totalEarned) > BigDouble.Zero;
    }

    // Perform the prestige action
    public void PerformPrestige()
    {
        BigDouble totalEarned = StatTracker.instance.TotalMoney;
        BigDouble gain = CalculatePrestigeGain(totalEarned);

        if (gain <= 0) return;

        prestigeCurrency += gain;

        ResetGameProgress();
        DataPersist.instance.SaveGameData();
    }

    // Reset all game progress except for prestige currency
    private void ResetGameProgress()
    {
        GetComponent<CurrencyManager>().ResetCurrency();
        GetComponent<UpgradeManager>().ResetUpgrades();
        GetComponent<GeneratorManager>().ResetGenerators();
        GetComponent<ClickManager>().moneyPerClick = BigDouble.One;
        GetComponent<IdleManager>().moneyPerSecond = BigDouble.Zero;
        StatTracker.instance.ResetRunStats();
    }

    // Get the prestige multiplier based on prestige currency
    public BigDouble GetPrestigeMultiplier()
    {
        return BigDouble.One + (prestigeCurrency * 0.01);
    }
}
