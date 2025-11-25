using UnityEngine;
using BreakInfinity;
using System.Collections;

public class IdleManager : MonoBehaviour
{
    public BigDouble moneyPerSecond;
    private CurrencyManager currencyManager;

    public const float UPDATE_INTERVAL = 0.1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(IdleTick());
        currencyManager = GetComponent<CurrencyManager>();
    }

    // Automaticaly adds money to our total
    private IEnumerator IdleTick()
    {
        while (true)
        {
            yield return new WaitForSeconds(UPDATE_INTERVAL);
            currencyManager.AddMoney(moneyPerSecond * UPDATE_INTERVAL);
            StatTracker.instance.moneyFromIdle += moneyPerSecond * UPDATE_INTERVAL;
        }
    }
}
