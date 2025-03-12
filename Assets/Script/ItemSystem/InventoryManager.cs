using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//InventoryManager 是所有物品相關腳本的核心
//管理物品清單
//當物品變更時通知UI

public class InventoryManager : MonoBehaviour
{
    //為了讓所有腳本都有Accessbility，一個接口的概念
    //Singleton 是一種設計模式，確保在遊戲中只有一個 InventoryManager 存在
    
    #region Singleton
    public static InventoryManager Instance;

    
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
}
