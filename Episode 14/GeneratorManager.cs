using UnityEngine;

public class GeneratorManager : MonoBehaviour
{
    public Generator[] generators;

    // Resets all generators to their initial state
    public void ResetGenerators()
    {
        foreach (Generator generator in generators)
        {
            generator.ResetLevel();
        }
    }
}
