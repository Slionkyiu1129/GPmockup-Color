using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemContainer : MonoBehaviour
{
    public Item thisItem;
    private bool isPlayerNearby = false;
    public string saveItemID; // 唯一的物品 ID

    private void Start()
    {
        // 如果物品已經被撿起，就隱藏它
        if (SaveManager.Instance.HasPickedUpItem(saveItemID))
        {
            gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("Pressed T");
            SaveManager.Instance.AddPickedUpItem(saveItemID);
            
            //Add item to inventory
            InventoryManager.Instance.Add(thisItem);
            InventoryManager.Instance.onInventoryCallBack();
            
            Destroy(gameObject);
        }
    }

    //Enter Trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            Debug.Log("PLayer entered trigger zone");
        }
    }

    //Exit Trigger
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            Debug.Log("Player left trigger zone");
        }
    }
}
