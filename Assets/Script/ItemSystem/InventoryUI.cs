using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public ItemSlot[] ItemSlots;
    public Transform InventoryParent;

    public GameObject selectionCursor;
    private int selectedSlotIndex = 0;
    private int columns = 2;
    private int itemCount => InventoryManager.Instance.ItemList.Count;

    private void Start()
    {
        InventoryManager.Instance.onInventoryCallBack += UpdateUI;
        ItemSlots = InventoryParent.GetComponentsInChildren<ItemSlot>();

        /*
        foreach (var slot in ItemSlots)
        {
            slot.gameObject.SetActive(false);
        }
        */
        
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
                ItemSlots[i].gameObject.SetActive(false);
            }
        }
    }

    private void MoveSelection(int vertical, int horizontal)
    {
        int newIndex = selectedSlotIndex + (vertical * columns) + horizontal;

        if (newIndex < 0 || newIndex >= itemCount)
            return;

        if ((selectedSlotIndex % columns == 0 && horizontal == -1) ||  
            ((selectedSlotIndex + 1) % columns == 0 && horizontal == 1)) 
        {
            return;
        }

        while (!ItemSlots[newIndex].gameObject.activeSelf)
        {
            newIndex += horizontal != 0 ? horizontal : vertical * columns;
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

/* 
private void MoveSelection(int rowChange, int colChange)
{
    int columns = 2;
    int rows = ItemSlots.Length / columns;

    int newRow = selectedSlotIndex / columns + rowChange;
    int newCol = selectedSlotIndex % columns + colChange;

    newRow = Mathf.Clamp(newRow, 0, rows - 1);
    newCol = Mathf.Clamp(newCol, 0, columns - 1);

    selectedSlotIndex = newRow * columns + newCol;
    UpdateCursorPosition();
}
*/