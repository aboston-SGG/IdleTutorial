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
            genComponent.playerClass = DataPersist.instance.saveData.playerClass;
            genComponent.generatorData = data;
            genComponent.monsterManager = GetComponent<MonsterManager>();
            genComponent.currencyManager = GetComponent<CurrencyManager>();
            genComponent.SetValues();
            generators.Add(genComponent);
            newGen.SetActive(false);
        }

        generators[0].gameObject.SetActive(true);
    }

    // Unlocks the next generator in the list after the specified generator has been purchased
    public void UnlockGenerators(Generator gen)
    {
        if(generators.IndexOf(gen) + 1 >= generators.Count) return;

        generators[generators.IndexOf(gen) + 1].gameObject.SetActive(true);
    }
}
