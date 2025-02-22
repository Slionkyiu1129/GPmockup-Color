using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchState : MonoBehaviour
{
    public Sprite switchOnSprite;  // 開啟時的圖片
    public Sprite switchOffSprite; // 關閉時的圖片
    private SpriteRenderer spriteRenderer;
    private bool isOn = false; // 開關狀態
    private bool isPlayerNearby = false; // 玩家是否在範圍內

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = switchOffSprite; // 預設為關閉
    }

    void Update()
    {
        // 當玩家在範圍內，且按下 Space 鍵時，切換開關
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.Space))
        {
            ToggleSwitch();
        }
    }

    void ToggleSwitch()
    {
        isOn = !isOn; // 切換狀態
        spriteRenderer.sprite = isOn ? switchOnSprite : switchOffSprite; // 切換圖片
    }

    // 當玩家進入開關範圍
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // 確保是玩家
        {
            isPlayerNearby = true;
        }
    }

    // 當玩家離開開關範圍
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
        }
    }
}
