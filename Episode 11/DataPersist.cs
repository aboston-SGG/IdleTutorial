using UnityEngine;
using System;
using BreakInfinity;
using System.Collections;
using System.IO;
using System.Security.Cryptography;

public class DataPersist : MonoBehaviour
{
    public static DataPersist instance;

    public CurrencyManager currencyManager;
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
    }

    // Gather all data to be saved into the saveData object
    public void RetrieveSaveData()
    {
        GameData data = saveData;

        data.money = currencyManager.money;
        data.clickUpgradeCount = clickUpgrade.count;
        data.idleUpgradeCount = idleUpgrade.count;
        data.generatorCount = generator.count;
        data.timePlayed = StatTracker.instance.timePlayed;
        data.moneyFromClicks = StatTracker.instance.moneyFromClicks;
        data.moneyFromIdle = StatTracker.instance.moneyFromIdle;
        data.clicks = StatTracker.instance.clicks;
    }

    // Save the game data to an encrypted file
    public void SaveGameData()
    {
        if(dataStream != null)
        {
            dataStream.Close();
        }

        Aes iAes = Aes.Create();

        savedKey = iAes.Key;

        string key = Convert.ToBase64String(savedKey);
        PlayerPrefs.SetString("Key", key);

        dataStream = new FileStream(path, FileMode.Create);

        byte[] inputIV = iAes.IV;

        dataStream.Write(inputIV, 0, inputIV.Length);

        CryptoStream iStream = new CryptoStream(dataStream, iAes.CreateEncryptor(iAes.Key, iAes.IV), CryptoStreamMode.Write);
    
        StreamWriter sw = new StreamWriter(iStream);

        string saveDataFile = string.Empty;
        saveDataFile = JsonUtility.ToJson(saveData);

        sw.Write(saveDataFile);
        sw.Close();
        dataStream.Close();
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
}
