using UnityEngine;
using TMPro;
using System;
using Unity.Collections;

public class UIManager : MonoBehaviour
{
    private CurrencyManager currencyManager;

    public TextMeshProUGUI moneyText;

    [Header("Stats")]
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI totalMoneyText;
    public TextMeshProUGUI clickMoneyText;
    public TextMeshProUGUI idleMoneyText;
    public TextMeshProUGUI clicksText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currencyManager = GetComponent<CurrencyManager>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCurrencyText();
        UpdateStatText();
    }

    // Updates our money display
    private void UpdateCurrencyText()
    {
        moneyText.text = $"Money: {CurrencyManager.SciNotToUSName(currencyManager.money)}";
    }

    // Opens a target menu
    public void OpenMenu(CanvasGroup menu)
    {
        menu.alpha = 1;
        menu.interactable = true;
        menu.blocksRaycasts = true;
    }

    // Closes a target menu
    public void CloseMenu(CanvasGroup menu)
    {
        menu.alpha = 0;
        menu.interactable = false;
        menu.blocksRaycasts = false;
    }

    // Updates the text in the stats panel
    private void UpdateStatText()
    {
        TimeSpan time = TimeSpan.FromSeconds(StatTracker.instance.timePlayed.ToDouble());
        timeText.text = "Time Spent: " + string.Format("{0:D2}:{1:D2}:{2:D2}", time.Hours, time.Minutes, time.Seconds);
        totalMoneyText.text = $"Total Money: {CurrencyManager.SciNotToUSName(StatTracker.instance.TotalMoney)}";
        clickMoneyText.text = $"Money from Clicks: {CurrencyManager.SciNotToUSName(StatTracker.instance.moneyFromClicks)}";
        idleMoneyText.text = $"Money from Idle: {CurrencyManager.SciNotToUSName(StatTracker.instance.moneyFromIdle)}";
        clicksText.text = $"Total Clicks: {CurrencyManager.SciNotToUSName(StatTracker.instance.clicks)}";
    }
}
