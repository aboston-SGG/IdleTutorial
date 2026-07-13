using UnityEngine;
using System;
using BreakInfinity;
using System.Collections;
using System.IO;
using System.Security.Cryptography;
using UnityEngine.SceneManagement;
using TMPro;

public class DataPersist : MonoBehaviour
{
    public static DataPersist instance;

    public CurrencyManager currencyManager;
    public MonsterManager monsterManager;
    public StageManager stageManager;
    public Upgrade clickUpgrade;
    public Upgrade idleUpgrade;
    public Generator generator;

    public GameData saveData = new();

    public const float SAVE_INTERVAL = 10f;

    #region File Management
    private string path;
    private byte[] savedKey;
    private FileStream dataStream;
    #endregion

    #region Offline Earnings
    public GameObject offlineEarningsPopup;
    public TextMeshProUGUI offlineTimeText;
    public TextMeshProUGUI offlineEarningsText;
    public BigDouble maxOfflineTime = 43200;
    public BigDouble minOfflineTime = 10;
    public float offlineModifier;
    #endregion


    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        path = Application.persistentDataPath + "/save.json";
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(AutoSave());
        LoadData();
        ApplyLoadData();
    }

    // Gather all data to be saved into the saveData object
    public void RetrieveSaveData()
    {
        GameData data = saveData;

        data.money = currencyManager.money;
        data.moneyPerSec = GetComponent<UIManager>().GetMoneyPerSecond();
        data.prestigeCurrency = PrestigeManager.instance.prestigeCurrency;
        data.clickUpgradeCount = clickUpgrade.count;
        data.idleUpgradeCount = idleUpgrade.count;
        data.generatorCount = generator.count;
        data.timePlayed = StatTracker.instance.timePlayed;
        data.totalDamage = StatTracker.instance.TotalDamage;
        data.damageFromClicks = StatTracker.instance.damageFromClicks;
        data.damageFromIdle = StatTracker.instance.damageFromIdle;
        data.clicks = StatTracker.instance.clicks;
        data.moneyEarned = StatTracker.instance.moneyEarned;
        data.monstersKilled = StatTracker.instance.monstersKilled;
        data.stagesCompleted = StatTracker.instance.stagesCompleted;
        data.currentMonsterIndex = monsterManager.monsters.IndexOf(monsterManager.currentMonster);
        data.currentMonsterHealth = monsterManager.currentMonsterHealth;
        data.stageNumber = stageManager.stageNumber;
        data.killCount = stageManager.killCount;
        data.currentStageIndex = Array.IndexOf(stageManager.stages, stageManager.currentStage);
    }

    // Save the game data to an encrypted file
    public void SaveGameData()
    {
        using Aes iAes = Aes.Create();

        savedKey = iAes.Key;
        PlayerPrefs.SetString("Key", Convert.ToBase64String(savedKey));

        using FileStream dataStream = new FileStream(path, FileMode.Create, FileAccess.Write);

        dataStream.Write(iAes.IV, 0, iAes.IV.Length);

        using CryptoStream iStream = new CryptoStream(dataStream, iAes.CreateEncryptor(), CryptoStreamMode.Write);
    
        using StreamWriter sw = new StreamWriter(iStream);

        string saveDataFile = JsonUtility.ToJson(saveData);

        sw.Write(saveDataFile);
    }

    // Check if a save file exists
    public bool TryLoad()
    {
        return File.Exists(path);
    }

    // Load the game data from the encrypted file
    public void LoadData()
    {
        if (TryLoad() && PlayerPrefs.HasKey("Key"))
        {
            try
            {
                savedKey = Convert.FromBase64String(PlayerPrefs.GetString("Key"));

                using FileStream dataStream = new FileStream(path, FileMode.Open);

                using Aes oAes = Aes.Create();

                byte[] outPutIV = new byte[oAes.IV.Length];
                int bytesread = dataStream.Read(outPutIV, 0, outPutIV.Length);

                if (bytesread != outPutIV.Length)
                {
                    Debug.LogError("Save File corrupted: IV length mismatch.");
                    return;
                }

                using CryptoStream oStream = new CryptoStream(dataStream, oAes.CreateDecryptor(savedKey, outPutIV), CryptoStreamMode.Read);

                using StreamReader sr = new StreamReader(oStream);

                string saveDataFile = sr.ReadToEnd();

                saveData = JsonUtility.FromJson<GameData>(saveDataFile);
                saveData.saveTime = File.GetLastWriteTime(path);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to load save data: {ex.Message}");
                RetrieveSaveData();
                SaveGameData();
            }
        }
        else
        {
            RetrieveSaveData();
            SaveGameData();
        }
    }

    // Apply the loaded data to the game state
    public void ApplyLoadData()
    {
        if(!TryLoad()) return;

        GameData data = saveData;

        currencyManager.AddMoney(data.money);
        PrestigeManager.instance.prestigeCurrency = data.prestigeCurrency;
        clickUpgrade.count = data.clickUpgradeCount;
        idleUpgrade.count = data.idleUpgradeCount;
        generator.count = data.generatorCount;
        StatTracker.instance.timePlayed = data.timePlayed;
        StatTracker.instance.damageFromClicks = data.damageFromClicks;
        StatTracker.instance.damageFromIdle = data.damageFromIdle;
        StatTracker.instance.clicks = data.clicks;
        StatTracker.instance.moneyEarned = data.moneyEarned;
        StatTracker.instance.monstersKilled = data.monstersKilled;
        StatTracker.instance.stagesCompleted = data.stagesCompleted;
        monsterManager.currentMonster = monsterManager.monsters[data.currentMonsterIndex];
        monsterManager.currentMonsterHealth = data.currentMonsterHealth;
        stageManager.stageNumber = data.stageNumber;
        stageManager.killCount = data.killCount;
        stageManager.currentStage = stageManager.stages[data.currentStageIndex];
        stageManager.backgroundImage.sprite = stageManager.currentStage.background;
        monsterManager.currentMonsterMaxHealth = monsterManager.currentMonster.baseHealth * BigDouble.Pow(monsterManager.currentMonster.scalingModifier, stageManager.stageNumber);
        monsterManager.currentMonsterReward = monsterManager.currentMonster.baseReward * BigDouble.Pow(monsterManager.currentMonster.scalingModifier, stageManager.stageNumber);
        ProcessOfflineGains(CalculateOfflineTime());

        foreach(Generator gen in GetComponent<GeneratorManager>().generators)
        {
            gen.SetValues();
        }

        foreach(Upgrade upg in GetComponent<UpgradeManager>().upgrades)
        {
            upg.SetValues();
        }
    }

    // Delete the saved game data
    public void DeleteData()
    {
        if (TryLoad())
        {
            File.Delete(path);
            PlayerPrefs.DeleteKey("Key");
            SceneManager.LoadScene(0);
        }
    }

    // Automatically save the game data at regular intervals
    public IEnumerator AutoSave()
    {
        while (true)
        {
            yield return new WaitForSeconds(SAVE_INTERVAL);
            RetrieveSaveData();
            SaveGameData();
        }
    }

    // Calculate the offline time since the last save
    public BigDouble CalculateOfflineTime()
    {
        GameData activeSave = saveData;

        BigDouble timeElapsed = DateTime.Now.Subtract(activeSave.saveTime).TotalSeconds;

        return timeElapsed > maxOfflineTime ? maxOfflineTime : (timeElapsed < 0 ? 0 : timeElapsed);
    }

    // Process offline earnings based on the elapsed time
    public void ProcessOfflineGains(BigDouble timeElapsed)
    {
        if(timeElapsed < minOfflineTime)
        {
            offlineEarningsPopup.SetActive(false);
            return;
        }

        BigDouble earnings = saveData.moneyPerSec * timeElapsed * offlineModifier;

        offlineTimeText.text = TimeSpan.FromSeconds(timeElapsed.ToDouble()).ToString(@"hh\:mm\:ss");
        offlineEarningsText.text = CurrencyManager.SciNotToUSName(earnings);
        currencyManager.AddMoney(earnings);
        StatTracker.instance.damageFromIdle += earnings;
        StatTracker.instance.timePlayed += timeElapsed;
    }
}
