using UnityEngine;
using BreakInfinity;
using System.Collections.Generic;

public class CurrencyManager : MonoBehaviour
{
    public BigDouble money { get; private set;}

    // Adds money
    public void AddMoney(BigDouble amount)
    {
        money += amount;
    }

    // Removes money (add safety check later)
    public void SubtractMoney(BigDouble amount)
    {
        money -= amount;
    }

    // Checks if we have enough money for purchase
    public bool CanAfford(BigDouble amount)
    {
        return money >= amount;
    }

    // Converts a BigDouble from a scientific notation format to a more readable format
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
}
