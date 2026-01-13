using UnityEngine;
using BreakInfinity;
using TMPro;
using UnityEngine.UI;

public class Generator : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI countText;
    public TextMeshProUGUI productionText;
    public TextMeshProUGUI costText;
    public Image progressBar;
    public Button buyButton;

    [Header("Generator Properties")]
    public string generatorName;
    public BigDouble baseCost;
    public BigDouble costMultiplier;
    public BigDouble count;
    private BigDouble cost;
    public BigDouble baseProduction;
    public BigDouble production;
    public float productionTime;
    private float currentTime;

    public CurrencyManager currencyManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetValues();
        nameText.text = generatorName;
        SetText();
        progressBar.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(currencyManager.CanAfford(cost))
        {
            buyButton.interactable = true;
        }
        else
        {
            buyButton.interactable = false;
        }

        HandleTime();
    }

    // Handle the production timing and progress bar
    public void HandleTime()
    {
        if (count < 1) return;

        currentTime += Time.deltaTime;
        progressBar.fillAmount = currentTime / productionTime;

        if(currentTime >= productionTime)
        {
            currentTime = 0;
            Produce();
        }
    }

    // Produce the generated currency
    public void Produce()
    {
        currencyManager.AddMoney(production);
    }

    // Purchase a new generator
    public void Purchase()
    {
        if(!currencyManager.CanAfford(cost)) return;

        currencyManager.SubtractMoney(cost);
        count++;
        SetValues();

        SetText();
    }

    // Reset the generator to its initial state
    public void ResetLevel()
    {
        count = BigDouble.Zero;
        currentTime = 0;
        SetValues();
        SetText();
    }

    // Set the cost and production values based on the current count
    public void SetValues()
    {
        cost = baseCost * BigDouble.Pow(costMultiplier, count);
        production = baseProduction * count;

        SetText();
    }

    // Update the UI text elements
    public void SetText()
    {
        countText.text = CurrencyManager.SciNotToUSName(count);
        costText.text = $"Cost: {CurrencyManager.SciNotToUSName(cost)}";
        productionText.text = CurrencyManager.SciNotToUSName(production * PrestigeManager.instance.GetPrestigeMultiplier() / productionTime) + " / sec";
    }
}
