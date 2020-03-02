using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Item", menuName = "Item")]
public class Item : ScriptableObject
{
    public enum TypeOfItem
    {
        Relic
    }

    //　アイテムの種類
    [SerializeField]
    private TypeOfItem kindOfItem;

    //　アイテムの名前
    [SerializeField]
    private int itemID;

    //　アイテムの名前
    [SerializeField]
    private string itemName;

    [SerializeField]
    private int itemCost;

    //　アイテムの情報
    [SerializeField]
    private string information;

    public TypeOfItem GetTypeOfItem()
    {
        return kindOfItem;
    }

    public int GetitemID()
    {
        return itemID;
    }

    public string GetItemName()
    {
        return itemName;
    }

    public int GetItemCost()
    {
        return itemCost;
    }

    public string GetInformation()
    {
        return information;
    }
}