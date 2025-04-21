using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemSystem/Item")]
public class Item : ScriptableObject
{
    public string ItemID;
    public string ItemName;
    public Sprite ItemImage;

    public virtual void UseItem()
    {
        //Method to use item
    }
}
