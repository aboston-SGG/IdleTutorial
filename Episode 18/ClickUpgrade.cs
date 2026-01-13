using UnityEngine;
using BreakInfinity;

public class ClickUpgrade : Upgrade
{
    public BigDouble clickPowerIncrease;
    public ClickManager clickManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Start()
    {
        base.Start();

        if (clickManager == null)
        {
            clickManager = FindAnyObjectByType<ClickManager>();
        }
    }

    // Buy the click upgrade
    public void BuyUpgrade()
    {
        clickManager.moneyPerClick += clickPowerIncrease;
        HandlePurchase();
    }

    // Update the click power based on the number of upgrades purchased
    public override void SetValues()
    {
        base.SetValues();
        clickManager.moneyPerClick = 1 + clickPowerIncrease * count;
    }
}
