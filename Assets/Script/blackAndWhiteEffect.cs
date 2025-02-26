using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Rendering;

public class blackAndWhiteEffect : MonoBehaviour
{
    public PostProcessVolume PostProcessingVolume;
    private ColorGrading colorGrading;
    private bool isBlackAndWhite = true;

    void Start()
    {
        if (PostProcessingVolume.profile.TryGetSettings(out colorGrading))
        {
            Debug.Log("黑白");
            colorGrading.saturation.value = -100; // 初始為黑白
        }
    }

    void Update()
    {
        if (colorGrading != null)
    {
        // 僅在 colorAdjustments 存在時，執行顏色變更
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("F");
            isBlackAndWhite = !isBlackAndWhite;
            colorGrading.saturation.value = isBlackAndWhite ? -100 : 0;
        }
    }
    else
    {
        Debug.LogWarning("ColorAdjustments 是 null, 請檢查 Post-Processing Volume 設定！");
    }
    }
}

