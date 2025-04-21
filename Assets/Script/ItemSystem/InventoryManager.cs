using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

//InventoryManager 是所有物品相關腳本的核心
//管理物品清單
//當物品變更時通知UI

public class InventoryManager : MonoBehaviour
{
    //為了讓所有腳本都有Accessbility，一個接口的概念
    //Singleton 是一種設計模式，確保在遊戲中只有一個 InventoryManager 存在
    
    #region Singleton
    public static InventoryManager Instance;
    //public SaveData saveData = new SaveData();
    
    private void Awake()
    {
        //確保只有一個 InventoryManager 存在
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        //確保 InventoryManager 不會在切換場景時被刪除，物品可以跨場景使用，不會被重置
        DontDestroyOnLoad(this);

        if (onInventoryCallBack == null)
        {
            onInventoryCallBack = delegate { }; // 確保不會是 NULL
        }

        Debug.Log("InventoryManager Initialized!");
    }
    #endregion

    public List<Item> ItemList = new List<Item>();
    public delegate void onInventoryChange();
    public onInventoryChange onInventoryCallBack;

    private void Start()
    {
        if (SaveManager.Instance == null)
        {
            Debug.LogError("SaveManager.Instance is NULL!");
        }
        LoadInventory(); // 嘗試讀取存檔
        Debug.Log("載入的物品數量: " + ItemList.Count);

        // 觸發 UI 更新，確保顯示正確
        onInventoryCallBack?.Invoke();
    }

    public void Add(Item newItem)
    {
        ItemList.Add(newItem);

        Debug.Log("Item added to inventory: " + newItem.ItemName);

        if (onInventoryCallBack != null)
        {
            onInventoryCallBack();
        }
        else
        {
            Debug.LogWarning("onInventoryCallBack is NULL! UI will not update.");
        }
    }

    public void Remove(Item oldItem)
    {
        ItemList.Remove(oldItem);
    }

    private void LoadInventory()
    {
        SaveData saveData = SaveManager.Instance.LoadGame();
        foreach (string itemID in saveData.pickedUpItems)
        {
            Debug.Log($"🔎 嘗試載入物品 ID: {itemID}");
            Item item = ItemDatabase.Instance.GetItemByID(itemID);
        
            if (item != null)
            {
                Debug.Log($"✅ 成功找到物品: {item.ItemName}");
                Add(item);
            }
            else
            {
                Debug.LogWarning($"❌ 找不到物品 ID：{itemID}");
            }
        }
    }

}
