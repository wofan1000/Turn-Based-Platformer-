using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Shop : MonoBehaviour
{
    public static Shop instance;

    public GameObject shopMenu;
    public GameObject buyMenu;
    public GameObject sellMenu;

    public Text goldText;

    public string[] itemsForSale;

    public ItemButton[] buyItemButtons,sellItemButtons;

    public Items selectedItem;

    public Text buyitenname, buyItemDisc, buyItemValue, sellItemName, sellItemValue, sellItemDisc;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M) && !shopMenu.activeInHierarchy)
        {
            OpenShop();
        }
    }

    public void OpenShop()
    {
        shopMenu.SetActive(true);
        openBuyMenu();
        GameManager.instance.shopActive = true;

        goldText.text = GameManager.instance.currentGold.ToString() + "g";
    }

    public void CloseShop()
    {
        shopMenu.SetActive(false);
        GameManager.instance.shopActive = false;
    }

    public void openBuyMenu()
    {
      

        buyMenu.SetActive(true);

        sellMenu.SetActive(false);

        for (int i = 0; i < buyItemButtons.Length; i++)
        {
            buyItemButtons[i].buttonValue = i;

            if (itemsForSale[i] != "")
            {
                buyItemButtons[i].itemImage.gameObject.SetActive(true);
                buyItemButtons[i].itemImage.sprite = GameManager.instance.GetItemDetails(itemsForSale[i]).itemSprite;
                buyItemButtons[i].amountText.text = "";
            }
            else
            {
                buyItemButtons[i].itemImage.gameObject.SetActive(false);
                buyItemButtons[i].amountText.text = "";
            }
        }
    }

    public void OpenSellMenu()
    {
        sellItemButtons[0].Press();

        buyMenu.SetActive(false);
        sellMenu.SetActive(true);
        ShowSellItems();
    }

    public void SelectBuyItem(Items buyItem)
    {
        selectedItem = buyItem;
        buyitenname.text = selectedItem.itemName;
        buyItemDisc.text = selectedItem.itemDisc;
        buyItemValue.text = "Value: " + selectedItem.value + "g";
    }

    public void SelectSellItem(Items sellItem)
    {
        selectedItem = sellItem;
        sellItemName.text = selectedItem.itemName;
        sellItemDisc.text = selectedItem.itemDisc;
        sellItemValue.text = "Value: " +Mathf.FloorToInt(selectedItem.value *.5f).ToString() + "g";
    }

    public void BuyItem()
    {
        if (selectedItem != null)
        {
            if (GameManager.instance.currentGold >= selectedItem.value)
            {
                GameManager.instance.currentGold -= selectedItem.value;

                GameManager.instance.AddItem(selectedItem.itemName);

                goldText.text = GameManager.instance.currentGold.ToString() + "g";
            }
        }
    }

    public void SellItem()
    {
        if(selectedItem != null)
        {
            GameManager.instance.currentGold += Mathf.FloorToInt(selectedItem.value * .5f);

            GameManager.instance.RemoveItem(selectedItem.itemName);
        }

        goldText.text = GameManager.instance.currentGold.ToString() + "g";

        ShowSellItems();
    }

    private void ShowSellItems()
    {
        GameManager.instance.SortItems();

        for (int i = 0; i < sellItemButtons.Length; i++)
        {
            sellItemButtons[i].buttonValue = i;

            if (GameManager.instance.itemsHeld[i] != "")
            {
                sellItemButtons[i].itemImage.gameObject.SetActive(true);
                sellItemButtons[i].itemImage.sprite = GameManager.instance.GetItemDetails(GameManager.instance.itemsHeld[i]).itemSprite;
                sellItemButtons[i].amountText.text = GameManager.instance.itenNum[i].ToString();
            }
            else
            {
                sellItemButtons[i].itemImage.gameObject.SetActive(false);
                sellItemButtons[i].amountText.text = "";
            }
        }
    }
}
