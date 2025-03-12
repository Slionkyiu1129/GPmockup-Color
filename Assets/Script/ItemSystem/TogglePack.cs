using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogglePack : MonoBehaviour
{
    public GameObject inventoryUI;
    public playerController player;
    
    void Start()
    {
        inventoryUI.SetActive(true);  // 確保 InventoryUI 啟動一次以初始化
        inventoryUI.SetActive(false);
    }

    void Update()
    {
        // 開啟背包
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ToggleInventory();
        }
    }


    private void ToggleInventory()
    {
        bool isActive = inventoryUI.activeSelf;
        inventoryUI.SetActive(!isActive);

        // 讓玩家不能移動
        if (player != null)
        {
            player.enabled = isActive; // 背包開啟時禁用移動，關閉時啟用移動
        }
    }
}
