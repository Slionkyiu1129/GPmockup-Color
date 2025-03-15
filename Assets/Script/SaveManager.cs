using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;
    private string savePath;
    public SaveData saveData = new SaveData();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            savePath = Path.Combine(Application.persistentDataPath, "saveData.json");
            LoadGame();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveGame()
    {
        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(savePath, json);
        Debug.Log("遊戲已存檔：" + savePath);
    }

    public SaveData LoadGame()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            saveData = JsonUtility.FromJson<SaveData>(json);
            Debug.Log("遊戲存檔已加載");
            return saveData;
        }
        else
        {
            Debug.LogWarning("找不到存檔，回傳新 SaveData 物件");
            return new SaveData(); // 這樣就不會報錯
        }
    }

    public void AddPickedUpItem(string itemID)
    {
        if (!saveData.pickedUpItems.Contains(itemID))
        {
            saveData.pickedUpItems.Add(itemID);
            SaveGame();
        }
    }

    public bool HasPickedUpItem(string itemID)
    {
        return saveData.pickedUpItems.Contains(itemID);
    }

    private void OnApplicationQuit()
    {
        SaveGame(); // **確保遊戲關閉時儲存資料**
    }
}
