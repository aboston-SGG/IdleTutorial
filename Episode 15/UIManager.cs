using UnityEngine;
using TMPro;
using System;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    private CurrencyManager currencyManager;

    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI perSecText;

    [Header("Stats")]
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI totalMoneyText;
    public TextMeshProUGUI clickMoneyText;
    public TextMeshProUGUI idleMoneyText;
    public TextMeshProUGUI clicksText;

    public List<CanvasGroup> menus;

    public TextMeshProUGUI prestigeButtonText;
    public TextMeshProUGUI prestigeCurrencyText;

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
        UpdatePrestigeUI();
    }

    // Update the currency display texts
    private void UpdateCurrencyText()
    {
        moneyText.text = $"Money: {CurrencyManager.SciNotToUSName(currencyManager.money)}";
        perSecText.text = $"Average per Second: {CurrencyManager.SciNotToUSName(StatTracker.instance.TotalMoney / StatTracker.instance.timePlayed)}";
    }

    // Open the specified menu and close others
    public void OpenMenu(CanvasGroup menu)
    {
        menu.alpha = 1;
        menu.interactable = true;
        menu.blocksRaycasts = true;

        CloseMenu(menu);
    }

    // Close all menus except the specified one
    public void CloseMenu(CanvasGroup menu)
    {
        foreach (CanvasGroup other in menus)
        {
            if (other == menu) continue;

            other.alpha = 0;
            other.interactable = false;
            other.blocksRaycasts = false;
        }
    }

    // Update the statistics display texts
    private void UpdateStatText()
    {
        TimeSpan time = TimeSpan.FromSeconds(StatTracker.instance.timePlayed.ToDouble());
        timeText.text = "Time Spent: " + string.Format("{0:D2}:{1:D2}:{2:D2}", time.Hours, time.Minutes, time.Seconds);
        totalMoneyText.text = $"Total Money: {CurrencyManager.SciNotToUSName(StatTracker.instance.TotalMoney)}";
        clickMoneyText.text = $"Money from Clicks: {CurrencyManager.SciNotToUSName(StatTracker.instance.moneyFromClicks)}";
        idleMoneyText.text = $"Money from Idle: {CurrencyManager.SciNotToUSName(StatTracker.instance.moneyFromIdle)}";
        clicksText.text = $"Total Clicks: {CurrencyManager.SciNotToUSName(StatTracker.instance.clicks)}";
    }

    // Update the prestige UI elements
    public void UpdatePrestigeUI()
    {
        prestigeButtonText.text = $"Prestige (Gain {CurrencyManager.SciNotToUSName(PrestigeManager.instance.CalculatePrestigeGain(StatTracker.instance.TotalMoney))})";
        prestigeCurrencyText.text = $"Prestige Currency: {CurrencyManager.SciNotToUSName(PrestigeManager.instance.prestigeCurrency)}";
    }
}
