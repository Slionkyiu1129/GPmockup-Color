using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemContainer : MonoBehaviour
{
    public Item thisItem;

    private void OnMouseDown()
    {
        Debug.Log("MouseDown");
        //Add item to inventory
        Destroy(gameObject);
    }
}
