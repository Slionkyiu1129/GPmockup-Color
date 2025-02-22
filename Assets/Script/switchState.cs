using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchState : MonoBehaviour
{
    private Sprite switchOnSprite;  // 開啟時的圖片
    private Sprite switchOffSprite; // 關閉時的圖片
    private SpriteRenderer backgroundRenderer; // 連結背景的 SpriteRenderer
    private Sprite backgroundOnSprite; // 開啟時的背景圖片
    private Sprite backgroundOffSprite; // 關閉時的背景圖片

    private SpriteRenderer spriteRenderer;
    private bool isOn = false;
    private bool isPlayerNearby = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = switchOffSprite; // 預設開關關閉
        spriteRenderer.sprite = Resources.Load<Sprite>("Picture/");
        GameObject backgroundObj = GameObject.Find("background");
        if (backgroundObj != null)
        {
            backgroundRenderer = backgroundObj.GetComponent<SpriteRenderer>(); // 預設背景圖片
        }
        if (backgroundRenderer != null)
        {
            backgroundRenderer.sprite = Resources.Load<Sprite>("Picture/openLight");
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
        spriteRenderer.sprite = Resources.Load<Sprite>(isOn ? "Picture/offLight2" : "Picture/");
        // 切換背景圖片
        if (backgroundRenderer != null)
        {
            backgroundRenderer.sprite = Resources.Load<Sprite>(isOn ? "Picture/offLightBlack" : "Picture/openLight");
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
