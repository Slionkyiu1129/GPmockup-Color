using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public List<string> pickedUpItems = new List<string>(); // 存放已撿起物品的 ID
}
