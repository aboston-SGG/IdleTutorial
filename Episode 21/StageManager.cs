using UnityEngine;
using TMPro;
using BreakInfinity;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    public Stage[] stages;
    public Stage currentStage;

    public BigDouble stageNumber = 1;

    public int killCount;

    private const int KILLS_TO_ADVANCE = 10;

    public Image backgroundImage;
    public TextMeshProUGUI stageText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentStage = stages[Random.Range(0, stages.Length)];
        backgroundImage.sprite = currentStage.background;
    }

    // Update is called once per frame
    void Update()
    {
        stageText.text = $"Stage {stageNumber}: {killCount}/{KILLS_TO_ADVANCE}";

        if(killCount >= KILLS_TO_ADVANCE)
        {
            AdvanceStage();
        }
    }

    // Advance to the next stage
    private void AdvanceStage()
    {
        killCount = 0;
        stageNumber++;
        StatTracker.instance.stagesCompleted++;
        currentStage = stages[Random.Range(0, stages.Length)];
        backgroundImage.sprite = currentStage.background;
    }

    public void ResetStages()
    {
        AdvanceStage();
        stageNumber = 0;
    }
}
