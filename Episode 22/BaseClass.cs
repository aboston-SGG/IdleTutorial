using UnityEngine;
using BreakInfinity;

[System.Serializable, CreateAssetMenu(fileName = "BaseClass", menuName = "Scriptable Objects/Base Class")]
public class BaseClass : ScriptableObject
{
    public string className;
    public string classDescription;
    public GeneratorData[] generators;
    public BigDouble baseDamagePerClick;
    public BigDouble baseDamagePerSecond;
    public BigDouble baseMoneyPerSecond;
}
