using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public Transform player; // 拖入玩家的 Transform
    //public float cameraWidth = 1200f;
    //public float cameraHeight = 1000f;
    public float mapWidth = 5000f;
    public float mapHeight = 5000f;

    private float halfWidth;
    private float halfHeight;

    void Start()
    {
        //halfWidth = 19f;
        //halfHeight = 20f;
    }

    void LateUpdate()
    {
        if (player == null) return;

        float targetX = Mathf.Clamp(player.position.x, halfWidth, 38 - halfWidth);
        float targetY = Mathf.Clamp(player.position.y, halfHeight, 40 - halfHeight);

        transform.position = new Vector3(targetX, targetY, transform.position.z);
    }
}
