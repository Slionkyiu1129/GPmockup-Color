using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    //���F���Ҧ��}������Accessbility�A�@�ӱ��f������
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
