using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{

    private List<Item> shopWindow = new List<Item>();
    private List<Item> possibleItems = new List<Item>();
    private GameObject[] buttons;
    private Button exitButton;
    private Text moneyText;

    private int itemsInWindow = 3;

    // Start is called before the first frame update

    void Start()
    {
        Debug.Log(ItemManager.instance.items.Count);
        //ボタンを取得
        buttons = GameObject.FindGameObjectsWithTag("Button");
        exitButton = GameObject.Find("ExitButton").GetComponent<Button>();
        moneyText = GameObject.Find("Money").GetComponent<Text>();
        UpdateMoney();

        //所持していないアイテムのコピー
        possibleItems = new List<Item>(ItemManager.instance.items);
        int maxAmount = possibleItems.Count;
        Debug.Log(possibleItems.Count);

        //売り物を3つ選択
        for (int i = 0; i < itemsInWindow && i < maxAmount; i++)
        {
            int index = Random.Range(0, possibleItems.Count);

            Item item =  possibleItems[index];
            possibleItems.RemoveAt(index);
            shopWindow.Add(item);

        }

        //選択肢にイベント登録
        for (int i = 0; i < shopWindow.Count; i++)
        {
            Button button = buttons[i].GetComponent<Button>();
            Item item = shopWindow[i];

            button.onClick.AddListener(() =>
            {
                button.interactable = false;
                ItemManager.instance.Buy(item);
                UpdateMoney();
                if (item.GetItemName() == "Score Up") Engine.instance.UpdateScore(100);
            });

            Text t = buttons[i].transform.Find("Text").GetComponent<Text>();
            string newText = item.GetItemName() + "\n" + item.GetItemCost() + "\n" + item.GetInformation();
            t.text = newText;

        }
    }


    //お金を減らす
    private void UpdateMoney()
    {
        moneyText.text = "Money: " + Engine.instance.Money;
    }
}
