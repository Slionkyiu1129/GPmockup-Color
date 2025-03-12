using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogglePack : MonoBehaviour
{
    public GameObject inventoryUI;
    public playerController player;
    
    void Start()
    {
        inventoryUI.SetActive(true);  // �T�O InventoryUI �Ұʤ@���H��l��
        inventoryUI.SetActive(false);
    }

    void Update()
    {
        // �}�ҭI�]
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ToggleInventory();
        }
    }


    private void ToggleInventory()
    {
        bool isActive = inventoryUI.activeSelf;
        inventoryUI.SetActive(!isActive);

        // �����a���ಾ��
        if (player != null)
        {
            player.enabled = isActive; // �I�]�}�ҮɸT�β��ʡA�����ɱҥβ���
        }
    }
}
