using UnityEngine;
using BreakInfinity;

public class StatTracker : MonoBehaviour
{
    public static StatTracker instance;

    [Header("Stats")]
    public BigDouble timePlayed;
    public BigDouble TotalDamage => damageFromClicks + damageFromIdle;
    public BigDouble damageFromClicks;
    public BigDouble damageFromIdle;
    public BigDouble clicks;
    public BigDouble moneyEarned;
    public BigDouble monstersKilled;
    public BigDouble stagesCompleted;

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        timePlayed += Time.deltaTime;
    }

    // Resets the run statistics
    public void ResetRunStats()
    {
        timePlayed = BigDouble.Zero;
        damageFromClicks = BigDouble.Zero;
        damageFromIdle = BigDouble.Zero;
        clicks = BigDouble.Zero;
        moneyEarned = BigDouble.Zero;
        monstersKilled = BigDouble.Zero;
        stagesCompleted = BigDouble.Zero;
    }
}
