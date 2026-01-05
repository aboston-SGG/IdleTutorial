using UnityEngine;
using BreakInfinity;
using System.Collections.Generic;

public class CurrencyManager : MonoBehaviour
{
    public BigDouble money { get; private set;}


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddMoney(BigDouble amount)
    {
        money += amount;
    }

    public void SubtractMoney(BigDouble amount)
    {
        money -= amount;
    }

    public bool CanAfford(BigDouble amount)
    {
        return money >= amount;
    }

    public static string SciNotToUSName(BigDouble value)
    {
        string displayNumber = $"{(value.Mantissa * BigDouble.Pow(10, value.Exponent % 3)):G3}";

        int suffixIndex = (int)BigDouble.Floor(BigDouble.Abs(value.Exponent)).ToDouble();
        string name = string.Empty;
        int suffixOffset = 0;

        if (value.Exponent < 33)
        {
            suffixIndex /= 3;
            name += $"{Suffixes.suffixes[suffixIndex]}";
        }
        else
        {
            suffixIndex = (suffixIndex - 3) / 3;
            int tempSuffixIndex = suffixIndex;
            List<int> indices = new List<int>();

            for(int i = 0; i < suffixIndex.ToString().Length; i++)
            {
                int lastNum = tempSuffixIndex % 10;
                indices.Add(lastNum);
                tempSuffixIndex /= 10;
                name += Suffixes.suffixParts[indices[i] + suffixIndex];
                suffixOffset += 10;
            }
        }

        return $"{displayNumber} {name}";
    }

    // Resets the currency to zero
    public void ResetCurrency()
    {
        money = BigDouble.Zero;
    }
}
