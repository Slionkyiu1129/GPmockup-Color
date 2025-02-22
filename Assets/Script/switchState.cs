using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchState : MonoBehaviour
{
    public Sprite switchOnSprite;  // 開啟時的圖片
    public Sprite switchOffSprite; // 關閉時的圖片
    public SpriteRenderer backgroundRenderer; // 連結背景的 SpriteRenderer
    public Sprite backgroundOnSprite; // 開啟時的背景圖片
    public Sprite backgroundOffSprite; // 關閉時的背景圖片

    private SpriteRenderer spriteRenderer;
    private bool isOn = false;
    private bool isPlayerNearby = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = switchOffSprite; // 預設開關關閉
        if (backgroundRenderer != null)
        {
            backgroundRenderer.sprite = backgroundOffSprite; // 預設背景圖片
        }
    }

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.Space))
        {
            ToggleSwitch();
        }
    }

    void ToggleSwitch()
    {
        isOn = !isOn;
        spriteRenderer.sprite = isOn ? switchOnSprite : switchOffSprite;

        // 切換背景圖片
        if (backgroundRenderer != null)
        {
            backgroundRenderer.sprite = isOn ? backgroundOnSprite : backgroundOffSprite;
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
        }
    }
}
