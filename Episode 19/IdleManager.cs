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

    private IEnumerator IdleTick()
    {
        while (true)
        {
            yield return new WaitForSeconds(UPDATE_INTERVAL);
            currencyManager.AddMoney(moneyPerSecond * UPDATE_INTERVAL);
            StatTracker.instance.damageFromIdle += moneyPerSecond * UPDATE_INTERVAL;
        }
    }
}
