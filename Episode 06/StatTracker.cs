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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
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
