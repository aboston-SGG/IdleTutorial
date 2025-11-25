using UnityEngine;
using BreakInfinity;

public class ClickManager : MonoBehaviour
{
    private CurrencyManager currencyManager;
    public BigDouble moneyPerClick = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currencyManager = GetComponent<CurrencyManager>();
    }

    // Called when we click our button
    public void MoneyClick()
    {
        currencyManager.AddMoney(moneyPerClick);
        StatTracker.instance.moneyFromClicks += moneyPerClick;
        StatTracker.instance.clicks++;
    }
}
