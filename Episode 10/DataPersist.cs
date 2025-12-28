using UnityEngine;
using System;
using BreakInfinity;
using System.Collections;

public class DataPersist : MonoBehaviour
{
    public static DataPersist instance;

    public CurrencyManager currencyManager;
    public Upgrade clickUpgrade;
    public Upgrade idleUpgrade;
    public Generator generator;

    public GameData saveData = new();

    public const float SAVE_INTERVAL = 10f;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(AutoSave());
    }

    public void RetrieveSaveData()
    {
        GameData data = saveData;

        data.money = currencyManager.money;
        data.clickUpgradeCount = clickUpgrade.count;
        data.idleUpgradeCount = idleUpgrade.count;
        data.generatorCount = generator.count;
        data.timePlayed = StatTracker.instance.timePlayed;
        data.moneyFromClicks = StatTracker.instance.moneyFromClicks;
        data.moneyFromIdle = StatTracker.instance.moneyFromIdle;
        data.clicks = StatTracker.instance.clicks;
    }

    public IEnumerator AutoSave()
    {
        while (true)
        {
            yield return new WaitForSeconds(SAVE_INTERVAL);
            RetrieveSaveData();
        }
    }
}
