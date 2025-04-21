using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase Instance;
    //public List<string> allItems = new List<string>(); // 存放遊戲中的所有物品
    //private Dictionary<string, Item> itemDict = new Dictionary<string, Item>();
    private Dictionary<string, Item> allItems = new Dictionary<string, Item>();
    private string savePath;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }

        LoadItemsFromJSON();
        if (allItems == null || allItems.Count == 0)
        {
            Debug.LogError("ItemDatabase 內沒有任何物品！");
        }
        else
        {
            Debug.Log("ItemDatabase 內有 " + allItems.Count + " 個物品");
        }
        /*
        itemDict = new Dictionary<string, Item>();
        foreach (var itemID in allItems)
        {
            if (itemDict.TryGetValue(itemID, out Item item))
            {
                itemDict[itemID] = item;
            }
            else
            {
                Debug.LogWarning($"⚠ 找不到 ID 為 {itemID} 的物品！");
            }
        }
        */
    }

    // 透過 itemID 找到對應的物品
    public Item GetItemByID(string itemID)
    {
        if (allItems.TryGetValue(itemID, out Item foundItem))
        {
            Debug.Log($"🔍 成功找到物品: {foundItem.ItemName} (ID: {itemID})");
            return foundItem;
        }
        else
        {
            Debug.LogWarning($"❌ 找不到 itemID: {itemID}");
            return null;
        }
        
    }

    private void LoadItemsFromJSON()
    {
        savePath = Path.Combine(Application.persistentDataPath, "saveData.json");
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            SaveData saveData = JsonUtility.FromJson<SaveData>(json); // 反序列化 JSON 成 SaveData 物件
            if (saveData != null && saveData.pickedUpItems != null)
            {
                allItems = new Dictionary<string, Item>();

            foreach (var itemID in saveData.pickedUpItems)
            {
                if (itemDatabase.TryGetValue(itemID, out Item item))
                {
                    allItems[itemID] = item; // 直接存 `Item` 物件
                }
                else
                {
                    Debug.LogWarning($"⚠ 找不到 ID 為 {itemID} 的物品！");
                }
            }
            Debug.Log("✅ 成功載入 JSON 物品列表");
            }
            else
            {
                Debug.LogError("❌ JSON 內容無效，無法載入物品！");
            }
        }
        else
        {
            Debug.LogError("❌ 找不到 items.json，無法載入物品！");
        }
        
    }
}

/*
        // 偵錯: 確保 `allItems` 內有物品
        if (allItems == null || allItems.Count == 0)
        {
            Debug.LogError("ItemDatabase 內沒有任何物品！");
        }   
        else
        {
            Debug.Log("ItemDatabase 內有 " + allItems.Count + " 個物品");
            foreach (var item in allItems)
            {
                Debug.Log($"物品: {item.ItemName} (ID: {item.ItemID})");
            }
        }
        */