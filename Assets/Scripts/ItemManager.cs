using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//rumetimeinitializationで作られる
public class ItemManager : MonoBehaviour
{
    public static ItemManager instance;
    public List<Item> items; //プレイ中減っていく
    public List<Item> ownedItems;
    private Dictionary<string, bool> hasItem = new Dictionary<string, bool>(); //hasItem.["far_sight"] enumがあれば、hasItem.[far_sight]?


    [SerializeField]
    private ItemDataBase itemDataBase;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        //items = new List<Item>(itemDataBase.GetItemLists());
    }

  
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        //Debug.Log(string.Join(", ", items.Select(obj => obj.ToString())));
        Initialize();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool HasItem(string name)
    {
        //foreach(Item item in ownedItems)
        //{
        //    if (item.GetItemName() == name) return true;
        //}

        //return false;

        return hasItem[name];
    }

    public void LogAllItems()
    {
        foreach (KeyValuePair<string, bool> pair in hasItem)
        {
            Debug.Log(pair.Key + ": " + pair.Value);
        }
    }

    public void HasNoItem()
    {
        hasItem.Clear();
        foreach (Item item in itemDataBase.GetItemLists())
        {
            hasItem.Add(item.GetItemName(), false);
        }
        ownedItems.Clear();
    }
    
    public void Buy(Item item)
    {
        if (Engine.instance.Money >= item.GetItemCost())
        {
            Engine.instance.Money -= item.GetItemCost();
            items.Remove(item);
            ownedItems.Add(item);
            hasItem[item.GetItemName()] = true;
            Debug.Log("Bought "+item.GetItemName());
        }

    }

    //itemsの初期化。
    public void Initialize()
    {
        items = new List<Item>(itemDataBase.GetItemLists());
        HasNoItem();
    }

}
