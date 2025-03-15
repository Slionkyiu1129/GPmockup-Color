using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase Instance;

    public List<Item> allItems = new List<Item>(); // 存放遊戲中的所有物品

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

    // 確保 `allItems` 內有物品
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
    }

    // 透過 itemID 找到對應的物品
    public Item GetItemByID(string itemID)
    {
        foreach (Item item in allItems)
        {
            if (item.ItemID.ToString() == itemID)
            {
                return item;
            }
        }
        return null; // 找不到物品
    }
}
