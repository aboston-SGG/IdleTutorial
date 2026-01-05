using UnityEngine;
using BreakInfinity;

public class PrestigeManager : MonoBehaviour
{
    public static PrestigeManager instance;

    public BigDouble prestigeCurrency;
    public BigDouble prestigeThreshold = 1e6;

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

    public BigDouble CalculatePrestigeGain(BigDouble totalEarned)
    {
        if (totalEarned < prestigeThreshold) return BigDouble.Zero;

        BigDouble gain = BigDouble.Sqrt(totalEarned / prestigeThreshold);
        return BigDouble.Floor(gain);
    }

    public bool CanPrestige(BigDouble totalEarned)
    {
        return CalculatePrestigeGain(totalEarned) > BigDouble.Zero;
    }

    public void PerformPrestige()
    {
        BigDouble totalEarned = StatTracker.instance.TotalMoney;
        BigDouble gain = CalculatePrestigeGain(totalEarned);

        if (gain <= 0) return;

        prestigeCurrency += gain;

        ResetGameProgress();
    }

    private void ResetGameProgress()
    {
        GetComponent<CurrencyManager>().ResetCurrency();
        GetComponent<UpgradeManager>().ResetUpgrades();
        GetComponent<GeneratorManager>().ResetGenerators();
        GetComponent<ClickManager>().moneyPerClick = BigDouble.One;
        GetComponent<IdleManager>().moneyPerSecond = BigDouble.Zero;
        StatTracker.instance.ResetRunStats();
    }
}
