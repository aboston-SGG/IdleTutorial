using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public Upgrade[] upgrades;

    // Resets all upgrades to their initial state
    public void ResetUpgrades()
    {
        foreach (Upgrade upgrade in upgrades)
        {
            upgrade.ResetUpgrade(); 
        }
    }
}
