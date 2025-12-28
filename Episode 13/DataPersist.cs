using UnityEngine;
using System;
using BreakInfinity;
using System.Collections;
using System.IO;
using System.Security.Cryptography;
using UnityEngine.SceneManagement;

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
        LoadData();
        ApplyLoadData();
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

    // Check if a save file exists
    public bool TryLoad()
    {
        return File.Exists(path);
    }

    // Load the game data from the encrypted file
    public void LoadData()
    {
        if (dataStream != null)
        {
            dataStream.Close();
        }

        if(TryLoad() && PlayerPrefs.HasKey("Key"))
        {
            savedKey = Convert.FromBase64String(PlayerPrefs.GetString("Key"));

            dataStream = new FileStream(path, FileMode.Open);

            Aes oAes = Aes.Create();

            byte[] outPutIV = new byte[oAes.IV.Length];

            dataStream.Read(outPutIV, 0, outPutIV.Length);

            CryptoStream oStream = new CryptoStream(dataStream, oAes.CreateDecryptor(savedKey, outPutIV), CryptoStreamMode.Read);

            StreamReader sr = new StreamReader(oStream);

            string saveDataFile = sr.ReadToEnd();

            sr.Close();

            saveData = JsonUtility.FromJson<GameData>(saveDataFile);
            saveData.saveTime = File.GetLastWriteTime(path);
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
        clickUpgrade.count = data.clickUpgradeCount;
        idleUpgrade.count = data.idleUpgradeCount;
        generator.count = data.generatorCount;
        StatTracker.instance.timePlayed = data.timePlayed;
        StatTracker.instance.moneyFromClicks = data.moneyFromClicks;
        StatTracker.instance.moneyFromIdle = data.moneyFromIdle;
        StatTracker.instance.clicks = data.clicks;
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
}
