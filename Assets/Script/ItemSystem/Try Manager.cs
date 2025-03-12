/*
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryUI : MonoBehaviour
{
    public ItemSlot[] ItemSlots;
    public Transform InventoryParent;
    public GameObject inventoryCanvas; // �I�] UI

    private int selectedIndex = 0;
    private bool isOpen = false;

    private void Start()
    {
        InventoryManager.Instance.onInventoryCallBack += UpdateUI;
        ItemSlots = InventoryParent.GetComponentsInChildren<ItemSlot>();
        inventoryCanvas.SetActive(false); // �w�]���� UI
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ToggleInventory();
        }

        if (isOpen)
        {
            HandleInventoryNavigation();
        }
    }

    public void UpdateUI()
    {
        for (int i = 0; i < ItemSlots.Length; i++)
        {
            if (i < InventoryManager.Instance.ItemList.Count)
            {
                ItemSlots[i].AddItem(InventoryManager.Instance.ItemList[i]);
            }
            else
            {
                ItemSlots[i].Clean();
            }
        }
    }

    private void ToggleInventory()
    {
        isOpen = !isOpen;
        inventoryCanvas.SetActive(isOpen);

        if (isOpen)
            LockPlayerMovement();
        else
            UnlockPlayerMovement();
    }

    private void HandleInventoryNavigation()
    {
        if (Input.GetKeyDown(KeyCode.W))
            MoveSelection(-1);
        if (Input.GetKeyDown(KeyCode.S))
            MoveSelection(1);
        if (Input.GetKeyDown(KeyCode.Space))
            UseSelectedItem();
    }

    private void MoveSelection(int direction)
    {
        if (ItemSlots.Length == 0) return;

        selectedIndex += direction;
        selectedIndex = Mathf.Clamp(selectedIndex, 0, ItemSlots.Length - 1);

        // ���G��e��ܪ� Slot
        EventSystem.current.SetSelectedGameObject(ItemSlots[selectedIndex].gameObject);
    }

    private void UseSelectedItem()
    {
        if (selectedIndex < InventoryManager.Instance.ItemList.Count)
        {
            Debug.Log("�ϥΪ��~�G" + InventoryManager.Instance.ItemList[selectedIndex].ItemName);
            ItemSlots[selectedIndex].UseItem();
        }
    }

    private void LockPlayerMovement()
    {
        PlayerMovement player = FindObjectOfType<PlayerMovement>();
        if (player != null)
        {
            player.enabled = false;
        }
    }

    private void UnlockPlayerMovement()
    {
        PlayerMovement player = FindObjectOfType<PlayerMovement>();
        if (player != null)
        {
            player.enabled = true;
        }
    }
}
*/


//�ª�
/*
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public ItemSlot[] ItemSlots;
    public Transform InventoryParent;

    private void Start()
    {
        InventoryManager.Instance.onInventoryCallBack += UpdateUI;
        ItemSlots = InventoryParent.GetComponentsInChildren<ItemSlot>();
    }

    public void UpdateUI()
    {
        //Update UI every time Item add or remove
        for(int i=0; i < ItemSlots.Length; i++)
        {
            if (i < InventoryManager.Instance.ItemList.Count)
            {
                ItemSlots[i].AddItem(InventoryManager.Instance.ItemList[i]);
            }
            else
            {
                ItemSlots[i].Clean();
            }
        }
    }
}

 */