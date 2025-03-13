using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public ItemSlot[] ItemSlots;
    public Transform InventoryParent;

    public GameObject selectionCursor; // UI 元素，表示選中的物品
    private int selectedSlotIndex = 0; // 當前選擇的欄位索引
    private int columns = 2; // 你的背包欄位每列有幾個 (需根據 UI 設定)
    private int itemCount => InventoryManager.Instance.ItemList.Count;

    private void Start()
    {
        /* Debug訊息
        if (InventoryManager.Instance != null)
        {
            InventoryManager.Instance.onInventoryCallBack += UpdateUI;
            Debug.Log("InventoryUI subscribed to onInventoryCallBack");
        }
        else
        {
            Debug.LogError("InventoryManager.Instance is NULL! InventoryUI cannot subscribe.");
        }

        // 確保一開始更新 UI
       
        for (int i = 0; i < ItemSlots.Length; i++)
        {
            ItemSlots[i].gameObject.SetActive(false); // 沒有物品時隱藏欄位
        }
        */
        
        InventoryManager.Instance.onInventoryCallBack += UpdateUI;
        ItemSlots = InventoryParent.GetComponentsInChildren<ItemSlot>();

        // 確保所有欄位初始為隱藏
        foreach (var slot in ItemSlots)
        {
            slot.gameObject.SetActive(false);
        }

        UpdateUI();

        UpdateCursorPosition();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)) MoveSelection(-1, 0); 
        if (Input.GetKeyDown(KeyCode.S)) MoveSelection(1, 0);  
        if (Input.GetKeyDown(KeyCode.A)) MoveSelection(0, -1); 
        if (Input.GetKeyDown(KeyCode.D)) MoveSelection(0, 1);  
        if (Input.GetKeyDown(KeyCode.Space)) UseSelectedItem();
    }

    public void UpdateUI()
    {
        int itemCount = InventoryManager.Instance.ItemList.Count;

        //Update UI every time Item add or remove
        for (int i = 0; i < ItemSlots.Length; i++)
        {
            if (i < itemCount)
            {
                ItemSlots[i].gameObject.SetActive(true);
                ItemSlots[i].AddItem(InventoryManager.Instance.ItemList[i]);
            }
            else
            {
                ItemSlots[i].Clean();
                ItemSlots[i].gameObject.SetActive(false); // 沒有物品時隱藏欄位
            }
        }
    }

    private void MoveSelection(int vertical, int horizontal)
    {
        int newIndex = selectedSlotIndex + (vertical * columns) + horizontal;

        // 確保新索引不超出範圍
        if (newIndex < 0 || newIndex >= itemCount)
            return;

        // 確保不會從最右邊跳到最左邊 (防止 A/D 錯位)
        if ((selectedSlotIndex % columns == 0 && horizontal == -1) ||  // 左邊界限制
            ((selectedSlotIndex + 1) % columns == 0 && horizontal == 1)) // 右邊界限制
        {
            return;
        }

        // 確保選擇的欄位是顯示中的 (避免選到 `SetActive(false)` 的欄位)
        while (!ItemSlots[newIndex].gameObject.activeSelf)
        {
            newIndex += horizontal != 0 ? horizontal : vertical * columns;

            // 如果超出範圍，就返回
            if (newIndex < 0 || newIndex >= itemCount)
                return;
        }

        selectedSlotIndex = newIndex;
        UpdateCursorPosition();
    }

    private void UpdateCursorPosition()
    {
        if (selectionCursor != null && selectedSlotIndex < ItemSlots.Length)
        {
            selectionCursor.transform.position = ItemSlots[selectedSlotIndex].transform.position;
        }
        for (int i = 0; i < ItemSlots.Length; i++)
        {
            ItemSlots[i].SetHighlight(i == selectedSlotIndex);
        }
    }

    private void UseSelectedItem()
    {
        if (selectedSlotIndex < ItemSlots.Length)
        {
            ItemSlots[selectedSlotIndex].UseItem();
        }
    }
}

/* 保留原本的寫法
private void MoveSelection(int rowChange, int colChange)
{
    int columns = 2; // 設定背包橫向有幾格
    int rows = ItemSlots.Length / columns;

    int newRow = selectedSlotIndex / columns + rowChange;
    int newCol = selectedSlotIndex % columns + colChange;

    newRow = Mathf.Clamp(newRow, 0, rows - 1);
    newCol = Mathf.Clamp(newCol, 0, columns - 1);

    selectedSlotIndex = newRow * columns + newCol;
    UpdateCursorPosition();
}
*/