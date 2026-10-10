using UnityEngine;
using BreakInfinity;

[System.Serializable, CreateAssetMenu(fileName = "GeneratorData", menuName = "Scriptable Objects/Generator Data")]

public class GeneratorData : ScriptableObject
{
    public string generatorName;
    public Sprite generatorIcon;
    public BigDouble baseCost;
    public BigDouble costMultiplier;
    public BigDouble count;
    public BigDouble baseProduction;

    public float productionTime;
}
