using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public CharStats[] playerStats;

    public bool gameMenuOpen, FadeIn, DialogOpen, battleActive, shopActive;

    public string[] itemsHeld;
    public int[] itenNum;
    public Items[] referenceItems;
    public int currentGold;
   

    // Start is called before the first frame update

    private void Awake()
    {
        instance = this;

        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        SortItems();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameMenuOpen || DialogOpen || FadeIn || battleActive || shopActive)
        {
            //PlayerController.canMove = false;
        } else
        {
            //PlayerController.canMove = true;
        }
        
    }

    public Items GetItemDetails(string itemToAdd)
    {
        for(int i = 0; i < referenceItems.Length; i++)
        {
            if(referenceItems[i].itemName == itemToAdd)
            {
                return referenceItems[i];
            }
        }

        return null;
    }

    public void SortItems()
    {
        bool itemSpace = true;

        while(itemSpace)
        {
            itemSpace = false;
            for (int i = 0; i < itemsHeld.Length - 1; i++)
            {
                if (itemsHeld[i] == "")
                {
                    itemsHeld[i] = itemsHeld[i + 1];
                    itemsHeld[i + 1] = "";

                    itenNum[i] = itenNum[i + 1];
                    itenNum[i + 1] = 0;

                    if(itemsHeld[i] != "")
                    {
                        itemSpace = true;
                    }

                }
            }
        }
    }

    public void AddItem(string itemToAdd)
    {
        int newItemPos = 0;
        bool foundSpace = false;

        for(int i = 0; i < itemsHeld.Length; i++)
        {
            newItemPos = i;
            i = itemsHeld.Length;
            foundSpace = true;
        }

        if(foundSpace)
        {
            bool itemExists = false;
            for(int i = 0; i < referenceItems.Length; i++)
            {
                if(referenceItems[i].itemName ==itemToAdd)
                {
                    itemExists = true;

                    i = referenceItems.Length;
                }
            }

            if(itemExists)
            {
                itemsHeld[newItemPos] = itemToAdd;
                itenNum[newItemPos]++;
            }  
        }
        GameMenu.instance.ShowItems();
    }

    public void RemoveItem(string itemToRemove)
    {
        bool foundItem = false;
        int itemPos = 0;
        for (int i = 0; i < itemsHeld.Length; i++)
        {
            if(itemsHeld[i] == itemToRemove)
            {
                foundItem = true;
                itemPos = 1;

                i = itemsHeld.Length;
            }
        }
        if(foundItem)
        {
            itenNum[itemPos]--;

            if(itenNum[itemPos] <= 0)
            {
                itemsHeld[itemPos] = "";
            }

            GameMenu.instance.ShowItems();
        }
    }

   
}
