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

    public void BuyUpgrade()
    {
        clickManager.moneyPerClick += clickPowerIncrease;
        HandlePurchase();
    }
}
