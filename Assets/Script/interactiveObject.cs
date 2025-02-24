using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // 加入 TextMeshPro 命名空間

public class InteractiveObject : MonoBehaviour
{
    public string message = "這是互動物件的訊息！"; // 顯示的文字
    private bool isPlayerNearby = false;
    public GameObject textPanel; // UI 面板 (顯示訊息)
    public TextMeshProUGUI messageText; // 這裡改成 TextMeshProUGUI

    void Start()
    {
        if (textPanel != null)
        {
            textPanel.SetActive(false); // 遊戲開始時隱藏文字
            Debug.Log("activeFalse");
        }
    }

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.Z))
        {
            ToggleMessage();
            Debug.Log("Z");
        }
    }

    void ToggleMessage()
    {
        if (textPanel != null)
        {
            bool isActive = textPanel.activeSelf;
            textPanel.SetActive(!isActive); // 切換訊息顯示
            messageText.text = message; // 使用 TextMeshProUGUI 來顯示訊息
            Debug.Log("Yes");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            if (textPanel != null)
            {
                textPanel.SetActive(false); // 玩家離開時自動關閉訊息
                //Debug.Log("Yes");
            }
        }
    }
}
