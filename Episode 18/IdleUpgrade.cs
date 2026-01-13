using UnityEngine;
using BreakInfinity;

public class IdleUpgrade : Upgrade
{
    public BigDouble idlePowerIncrease;
    public IdleManager idleManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Start()
    {
        base.Start();

        if (idleManager == null)
        {
            idleManager = FindAnyObjectByType<IdleManager>();
        }
    }

    // Buy the idle upgrade
    public void BuyUpgrade()
    {
        idleManager.moneyPerSecond += idlePowerIncrease;
        HandlePurchase();
    }

    // Update the idle power based on the number of upgrades purchased
    public override void SetValues()
    {
        base.SetValues();
        idleManager.moneyPerSecond = idlePowerIncrease * count;
    }
}
