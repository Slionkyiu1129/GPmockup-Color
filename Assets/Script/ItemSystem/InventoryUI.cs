using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public ItemSlot[] ItemSlots;
    public Transform InventoryParent;

    public GameObject selectionCursor; // UI �����A��ܿ襤�����~
    private int selectedSlotIndex = 0; // ��e��ܪ�������
    private int columns = 2; // �A���I�]���C�C���X�� (�ݮھ� UI �]�w)
    private int itemCount => InventoryManager.Instance.ItemList.Count;

    private void Start()
    {
        /* Debug�T��
        if (InventoryManager.Instance != null)
        {
            InventoryManager.Instance.onInventoryCallBack += UpdateUI;
            Debug.Log("InventoryUI subscribed to onInventoryCallBack");
        }
        else
        {
            Debug.LogError("InventoryManager.Instance is NULL! InventoryUI cannot subscribe.");
        }

        // �T�O�@�}�l��s UI
       
        for (int i = 0; i < ItemSlots.Length; i++)
        {
            ItemSlots[i].gameObject.SetActive(false); // �S�����~���������
        }
        */
        
        InventoryManager.Instance.onInventoryCallBack += UpdateUI;
        ItemSlots = InventoryParent.GetComponentsInChildren<ItemSlot>();

        // �T�O�Ҧ�����l������
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
                ItemSlots[i].gameObject.SetActive(false); // �S�����~���������
            }
        }
    }

    private void MoveSelection(int vertical, int horizontal)
    {
        int newIndex = selectedSlotIndex + (vertical * columns) + horizontal;

        // �T�O�s���ޤ��W�X�d��
        if (newIndex < 0 || newIndex >= itemCount)
            return;

        // �T�O���|�q�̥k�����̥��� (���� A/D ����)
        if ((selectedSlotIndex % columns == 0 && horizontal == -1) ||  // ����ɭ���
            ((selectedSlotIndex + 1) % columns == 0 && horizontal == 1)) // �k��ɭ���
        {
            return;
        }

        // �T�O��ܪ����O��ܤ��� (�קK��� `SetActive(false)` �����)
        while (!ItemSlots[newIndex].gameObject.activeSelf)
        {
            newIndex += horizontal != 0 ? horizontal : vertical * columns;

            // �p�G�W�X�d��A�N��^
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

/* �O�d�쥻���g�k
private void MoveSelection(int rowChange, int colChange)
{
    int columns = 2; // �]�w�I�]��V���X��
    int rows = ItemSlots.Length / columns;

    int newRow = selectedSlotIndex / columns + rowChange;
    int newCol = selectedSlotIndex % columns + colChange;

    newRow = Mathf.Clamp(newRow, 0, rows - 1);
    newCol = Mathf.Clamp(newCol, 0, columns - 1);

    selectedSlotIndex = newRow * columns + newCol;
    UpdateCursorPosition();
}
*/