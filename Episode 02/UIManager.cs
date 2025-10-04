using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    private CurrencyManager currencyManager;

    public TextMeshProUGUI moneyText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currencyManager = GetComponent<CurrencyManager>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCurrencyText();
    }

    // Updates our money display
    private void UpdateCurrencyText()
    {
        moneyText.text = $"Money: {CurrencyManager.SciNotToUSName(currencyManager.money)}";
    }
}
