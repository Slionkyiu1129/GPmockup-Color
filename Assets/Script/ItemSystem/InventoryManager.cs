using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    //為了讓所有腳本都有Accessbility，一個接口的概念
    #region Singleton
    public static InventoryManager Instance;
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(this);
    }
    #endregion


    public List<Item> ItemList;
    public delegate void onInventoryChange();
    public onInventoryChange onInventoryCallBack;

    public void Add(Item newItem)
    {
        ItemList.Add(newItem);
    }

    public void Remove(Item oldItem)
    {
        ItemList.Remove(oldItem);
    }
}
