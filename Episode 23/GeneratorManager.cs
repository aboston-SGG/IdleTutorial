using System.Collections.Generic;
using UnityEngine;

public class GeneratorManager : MonoBehaviour
{
    public List<Generator> generators;
    public GeneratorData[] generatorData;

    public GameObject generatorPrefab;
    public Transform generatorParent;

    // Resets all generators to their initial state
    public void ResetGenerators()
    {
        foreach (Generator generator in generators)
        {
            generator.ResetLevel();
        }
    }

    // Creates generator instances based on the provided generator data
    public void CreateGenerators()
    {
        foreach(GeneratorData data in generatorData)
        {
            GameObject newGen = Instantiate(generatorPrefab, generatorParent);
            Generator genComponent = newGen.GetComponent<Generator>();
            genComponent.generatorData = data;
            genComponent.currencyManager = GetComponent<CurrencyManager>();
            genComponent.SetValues();
            generators.Add(genComponent);
        }
    }
}
