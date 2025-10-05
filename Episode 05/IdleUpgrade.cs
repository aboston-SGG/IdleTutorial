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

    // Increases idle power
    public void BuyUpgrade()
    {
        idleManager.moneyPerSecond += idlePowerIncrease;
        HandlePurchase();
    }
}
