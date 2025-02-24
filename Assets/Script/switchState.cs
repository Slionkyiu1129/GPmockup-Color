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

    // 新增可互動物件
    public GameObject interactiveObject;
    private SpriteRenderer interactiveRenderer;
    private Sprite interactiveOnSprite;
    private Sprite interactiveOffSprite;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = Resources.Load<Sprite>("Picture/");

        // 設定背景
        GameObject backgroundObj = GameObject.Find("background");
        if (backgroundObj != null)
        {
            backgroundRenderer = backgroundObj.GetComponent<SpriteRenderer>();
            backgroundRenderer.sprite = Resources.Load<Sprite>("Picture/openLight1");
        }

        // 設定互動物件
        if (interactiveObject != null)
        {
            interactiveRenderer = interactiveObject.GetComponent<SpriteRenderer>();
            interactiveOnSprite = Resources.Load<Sprite>("Picture/interactiveOn");
            interactiveOffSprite = Resources.Load<Sprite>("Picture/interactiveOff");
            // 確保圖片有載入成功
            Debug.Log("interactiveOnSprite: " + (interactiveOnSprite != null));
            Debug.Log("interactiveOffSprite: " + (interactiveOffSprite != null));

            // 預設顯示關閉狀態
            interactiveRenderer.sprite = interactiveOffSprite;
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
            backgroundRenderer.sprite = Resources.Load<Sprite>(isOn ? "Picture/offLight1" : "Picture/openLight1");
        }

        // 切換互動物件的圖片
        if (interactiveRenderer != null)
        {
            interactiveRenderer.sprite = isOn ? interactiveOnSprite : interactiveOffSprite;
        }

        // 開關開啟後，開始 10 秒計時
        if (isOn)
        {
            StartCoroutine(SwitchOffAfterDelay(10f));
        }
    }

    IEnumerator SwitchOffAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // 10 秒後關閉開關
        isOn = false;
        spriteRenderer.sprite = switchOffSprite;
        spriteRenderer.sprite = Resources.Load<Sprite>("Picture/");
        
        if (backgroundRenderer != null)
        {
            backgroundRenderer.sprite = Resources.Load<Sprite>("Picture/openLight");
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
