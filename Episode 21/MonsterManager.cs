using UnityEngine;
using BreakInfinity;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class MonsterManager : MonoBehaviour
{
    public List<Monster> monsters;
    public Monster currentMonster;

    public BigDouble currentMonsterHealth;
    public BigDouble currentMonsterMaxHealth;
    public BigDouble currentMonsterReward;

    public Image monsterButtonImage;
    public Button monsterButton;

    public Image healthBar;
    public TextMeshProUGUI healthText;

    public CurrencyManager currencyManager;

    public StageManager stageManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(currencyManager == null)
        {
            currencyManager = GetComponent<CurrencyManager>();
        }

        if(stageManager == null)
        {
            stageManager = GetComponent<StageManager>();
        }

        GetNewMonster();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = (float)(currentMonsterHealth / currentMonsterMaxHealth).ToDouble();
        healthText.text = $"{CurrencyManager.SciNotToUSName(currentMonsterHealth)} / {CurrencyManager.SciNotToUSName(currentMonsterMaxHealth)}";

        if(currentMonsterHealth <= 0)
        {
            ProcessMonsterDeath();
            GetNewMonster();
        }
    }

    // Get a new monster from the list of monsters
    public void GetNewMonster()
    {
        monsters.Clear();
        monsters.AddRange(stageManager.currentStage.monsters);
        currentMonster = monsters[Random.Range(0, monsters.Count)];
        currentMonsterMaxHealth = currentMonster.baseHealth * BigDouble.Pow(currentMonster.scalingModifier, stageManager.stageNumber);
        currentMonsterHealth = currentMonsterMaxHealth;
        currentMonsterReward = currentMonster.baseReward * BigDouble.Pow(currentMonster.scalingModifier, stageManager.stageNumber);
        monsterButtonImage.sprite = currentMonster.monsterSprite;

        monsterButton.spriteState = new SpriteState
        {
            pressedSprite = currentMonster.damageSprite
        };
    }

    // Process the death of the current monster and reward the player
    private void ProcessMonsterDeath()
    {
        currencyManager.AddMoney(currentMonsterReward);
        StatTracker.instance.moneyEarned += currentMonsterReward;
        StatTracker.instance.monstersKilled++;
        stageManager.killCount++;
    }

    // Damage the current monster by the specified amount
    public void DamageMonster(BigDouble damage)
    {
        currentMonsterHealth -= damage;
    }
}
