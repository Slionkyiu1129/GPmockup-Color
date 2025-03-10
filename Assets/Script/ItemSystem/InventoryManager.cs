using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//InventoryManager �O�Ҧ����~�����}�����֤�
//�޲z���~�M��
//���~�ܧ�ɳq��UI

public class InventoryManager : MonoBehaviour
{
    //���F���Ҧ��}������Accessbility�A�@�ӱ��f������
    //Singleton �O�@�س]�p�Ҧ��A�T�O�b�C�����u���@�� InventoryManager �s�b

    #region Singleton
    public static InventoryManager Instance;

    private void Awake()
    {
        //�T�O�u���@�� InventoryManager �s�b
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        //�T�O InventoryManager ���|�A���������ɳQ�R���A���~�i�H������ϥΡA���|�Q���m
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
