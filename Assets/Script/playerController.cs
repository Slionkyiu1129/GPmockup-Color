using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // 獲取 SpriteRenderer

        string lastScene = PlayerPrefs.GetString("LastScene", "");
        if (PlayerPrefs.HasKey("LastX") && PlayerPrefs.HasKey("LastY"))
        {
            float x = PlayerPrefs.GetFloat("LastX");
            float y = PlayerPrefs.GetFloat("LastY");
            if(lastScene == "FirstScene" ){
                transform.position = new Vector3(x, -y-1, 0); // 設定玩家新位置
            }
            else{
                transform.position = new Vector3(x, -y+1, 0); // 設定玩家新位置
            }
        }
    }

    void Update()
    {
        // 讀取輸入
        movement.x = Input.GetAxisRaw("Horizontal"); 
        movement.y = Input.GetAxisRaw("Vertical");   

        // 翻轉角色
        if (movement.x > 0) 
            spriteRenderer.flipX = false; // 向右
        else if (movement.x < 0) 
            spriteRenderer.flipX = true;  // 向左
    }

    void FixedUpdate()
    {
        // 移動角色
        rb.velocity = movement.normalized * moveSpeed;
    }
}
