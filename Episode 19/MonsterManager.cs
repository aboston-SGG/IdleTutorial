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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(currencyManager == null)
        {
            currencyManager = GetComponent<CurrencyManager>();
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
        currentMonster = monsters[Random.Range(0, monsters.Count - 1)];
        currentMonsterMaxHealth = currentMonster.baseHealth;
        currentMonsterHealth = currentMonsterMaxHealth;
        currentMonsterReward = currentMonster.baseReward;
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
    }

    // Damage the current monster by the specified amount
    public void DamageMonster(BigDouble damage)
    {
        currentMonsterHealth -= damage;
    }
}
