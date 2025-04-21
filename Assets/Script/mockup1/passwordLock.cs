using UnityEngine;
using TMPro;

public class PasswordLock : MonoBehaviour
{
    public GameObject passwordPanel; // 密碼輸入 UI
    public TMP_InputField passwordInput; // 密碼輸入框
    public string correctPassword = "1234"; // 正確密碼
    private bool isPlayerNearby = false;

    public GameObject interactiveObject; // 需要顯示的物件

    void Start()
    {
        passwordPanel.SetActive(false); // 預設隱藏密碼輸入 UI
        if (interactiveObject != null)
        {
            interactiveObject.SetActive(false); // 預設隱藏互動物件
        }
    }

    void Update()
    {
        // 玩家靠近並按下 Z 開啟密碼輸入面板
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.Z))
        {
            OpenPasswordPanel();
        }

        // 按 Enter 確認密碼
        if (passwordPanel.activeSelf && Input.GetKeyDown(KeyCode.Return))
        {
            CheckPassword();
        }
    }

    void OpenPasswordPanel()
    {
        passwordPanel.SetActive(true);
        passwordInput.text = ""; // 清空輸入框
        passwordInput.ActivateInputField(); // 讓玩家直接輸入
    }

    void CheckPassword()
    {
        if (passwordInput.text == correctPassword)
        {
            Debug.Log("密碼正確，顯示互動物件！");
            passwordPanel.SetActive(false);

            // 顯示 interactiveObject
            if (interactiveObject != null)
            {
                interactiveObject.SetActive(true);
            }
        }
        else
        {
            Debug.Log("密碼錯誤！");
            passwordInput.text = "";
            passwordInput.ActivateInputField(); // 讓玩家重新輸入
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
            passwordPanel.SetActive(false); // 玩家離開時關閉 UI
        }
    }
}
