using UnityEngine;

[CreateAssetMenu(fileName = "New Stage", menuName = "Scriptable Objects/Stage")]
public class Stage : ScriptableObject
{
    public Monster[] monsters;
    public Sprite background;
}
