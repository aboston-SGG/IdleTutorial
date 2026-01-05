using UnityEngine;
using BreakInfinity;
using UnityEngine.UI;
using TMPro;

public class Upgrade : MonoBehaviour
{
    public string upgradeName;
    public string upgradeDescription;

    public BigDouble upgradeCost;
    public BigDouble baseCost;
    public BigDouble count;
    public BigDouble costModifier;

    public CurrencyManager currencyManager;

    public bool singlePurchase = false;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI costText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public virtual void Start()
    {
        if(currencyManager == null)
        {
            currencyManager = FindAnyObjectByType<CurrencyManager>();
        }

        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Button>().interactable = currencyManager.CanAfford(upgradeCost);
    }

    public void UpdateUI()
    {
        nameText.text = upgradeName;
        descriptionText.text = upgradeDescription;
        costText.text = $"Cost: {CurrencyManager.SciNotToUSName(upgradeCost)}";
    }

    public void HandlePurchase()
    {
        currencyManager.SubtractMoney(upgradeCost);

        if (!singlePurchase)
        {
            count++;
            upgradeCost = baseCost * BigDouble.Pow(costModifier, count);
            UpdateUI();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Resets the upgrade to its initial state
    public void ResetUpgrade()
    {
        count = BigDouble.Zero;
        upgradeCost = baseCost;
        UpdateUI();
    }
}
