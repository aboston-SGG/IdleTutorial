using UnityEngine;
using TMPro;
using System;
using System.Collections.Generic;
using BreakInfinity;

public class UIManager : MonoBehaviour
{
    private CurrencyManager currencyManager;

    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI perSecText;

    [Header("Stats")]
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI totalDamageText;
    public TextMeshProUGUI clickMoneyText;
    public TextMeshProUGUI idleMoneyText;
    public TextMeshProUGUI clicksText;
    public TextMeshProUGUI moneyEarnedText;
    public TextMeshProUGUI monstersKilledText;
    public TextMeshProUGUI stagesCompletedText;

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
        perSecText.text = $"Money Per Second: {CurrencyManager.SciNotToUSName(GetMoneyPerSecond())}";
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
        totalDamageText.text = $"Total Damage: {CurrencyManager.SciNotToUSName(StatTracker.instance.TotalDamage)}";
        clickMoneyText.text = $"Money from Clicks: {CurrencyManager.SciNotToUSName(StatTracker.instance.damageFromClicks)}";
        idleMoneyText.text = $"Money from Idle: {CurrencyManager.SciNotToUSName(StatTracker.instance.damageFromIdle)}";
        clicksText.text = $"Total Clicks: {CurrencyManager.SciNotToUSName(StatTracker.instance.clicks)}";
        moneyEarnedText.text = $"Total Money Earned: {CurrencyManager.SciNotToUSName(StatTracker.instance.moneyEarned)}";
        monstersKilledText.text = $"Monsters Killed: {CurrencyManager.SciNotToUSName(StatTracker.instance.monstersKilled)}";
        stagesCompletedText.text = $"Stages Completed: {CurrencyManager.SciNotToUSName(StatTracker.instance.stagesCompleted)}";
    }

    // Update the prestige UI elements
    public void UpdatePrestigeUI()
    {
        prestigeButtonText.text = $"Prestige (Gain {CurrencyManager.SciNotToUSName(PrestigeManager.instance.CalculatePrestigeGain(StatTracker.instance.TotalDamage))})";
        prestigeCurrencyText.text = $"Prestige Currency: {CurrencyManager.SciNotToUSName(PrestigeManager.instance.prestigeCurrency)}";
    }

    // Calculate the total money earned per second from all sources
    public BigDouble GetMoneyPerSecond()
    {
        BigDouble moneyPerSec = BigDouble.Zero;

        moneyPerSec += GetComponent<IdleManager>().moneyPerSecond;

        foreach(Generator gen in GetComponent<GeneratorManager>().generators)
        {
            moneyPerSec += gen.production / gen.generatorData.productionTime;
        }

        return moneyPerSec * PrestigeManager.instance.GetPrestigeMultiplier();
    }

    public void AssignPlayerClass(BaseClass classToAssign)
    {
        DataPersist.instance.saveData.playerClass = classToAssign;
        GetComponent<IdleManager>().damagePerSecond = classToAssign.baseDamagePerSecond;
        GetComponent<IdleManager>().moneyPerSecond = classToAssign.baseMoneyPerSecond;
        GetComponent<ClickManager>().damagePerClick = classToAssign.baseDamagePerClick;
    }
}
