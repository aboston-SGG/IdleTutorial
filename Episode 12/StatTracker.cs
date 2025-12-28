using UnityEngine;
using BreakInfinity;

public class StatTracker : MonoBehaviour
{
    public static StatTracker instance;

    [Header("Stats")]
    public BigDouble timePlayed;
    public BigDouble TotalMoney => moneyFromClicks + moneyFromIdle;
    public BigDouble moneyFromClicks;
    public BigDouble moneyFromIdle;
    public BigDouble clicks;

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
}
