using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameMenu : MonoBehaviour
{
    public GameObject theMenu;

    private CharStats[] playerStats;
    public Text[] nameText, hpText, lvlText, mpText, exptext;
    public Slider[] expSlider;
    public Image[] charImage;
    public GameObject[] charStatHolder;
    public GameObject[] windows;

    public GameObject[] statusButtons;

    public Text statsName, statsHP, statsMP, StatsStrength, statsDefense, statsEXPtoNextLvl;

    public Image statsImage;

    public ItemButton[] itemButtons;

    public static GameMenu instance;

    public string selectedItem;

    public Items activeItem;
    public Text itemname;
    public Text itemDisc, useButtonText;

    public GameObject itemCharChiceMenu;
    public Text[] itemCharChoiceNames;
    public Text goldText;

    public string levelSelect, mainMenu;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Pause"))
        {
            if(theMenu.activeInHierarchy)
            {
                // theMenu.SetActive(false);
                //GameManager.instance.gameMenuOpen = false;
                Time.timeScale = 1f;
                CloseMenu();
            }
            else
            {
                theMenu.SetActive(true);
                updateMainStats();
                GameManager.instance.gameMenuOpen = true;
                Time.timeScale = 0f;
                
            }
        }
    }

    public void updateMainStats()
    {
        playerStats = GameManager.instance.playerStats;

        for(int i = 0; i < playerStats.Length; i++)
        {
            if(playerStats[i].gameObject.activeInHierarchy)
            {
                charStatHolder[i].SetActive(true);

                nameText[i].text = playerStats[i].charName;
                hpText[i].text = "HP:" + playerStats[i].currentHP + "/" + playerStats[i].maxHP;
                mpText[i].text = "MP:" + playerStats[i].currentMP + "/" + playerStats[i].maxMP;
                lvlText[i].text = "Lvl." + playerStats[i].playrtLvl;
                exptext[i].text =  "" + playerStats[i].currentEXP + "/" + playerStats[i].expToNextLvl[playerStats[i].playrtLvl];
                expSlider[i].maxValue = playerStats[i].expToNextLvl[playerStats[i].playrtLvl];
                expSlider[i].value = playerStats[i].currentEXP;
                charImage[i].sprite = playerStats[i].charImage;
            } else
            {
                charStatHolder[i].SetActive(false);
            }
        }
        goldText.text = GameManager.instance.currentGold.ToString() + "g";
    }

     public void ToggleWindow(int windownum)
    {
        updateMainStats();
        for(int i = 0; i < windows.Length; i++)
        {
            if(i == windownum)
            {
                windows[i].SetActive(!windows[i].activeInHierarchy);
            } else
            {
                windows[i].SetActive(false);
            }
        }
        itemCharChiceMenu.SetActive(false);
    }
    public void CloseMenu()
    {
        for (int i = 0; i < windows.Length; i++)
        {
            windows[i].SetActive(false);
        }
        theMenu.SetActive(false);
        GameManager.instance.gameMenuOpen = false;

        itemCharChiceMenu.SetActive(false);
    }

    public void openStatus()
    {
        updateMainStats();
        StatusChar(0);

        for(int i = 0; i < statusButtons.Length; i++)
        {
            statusButtons[i].SetActive(playerStats[i].gameObject.activeInHierarchy);
            statusButtons[i].GetComponentInChildren<Text>().text = playerStats[i].charName;
        }
    }

    public void StatusChar(int selectedChar)
    {
        statsName.text = playerStats[selectedChar].charName;
        statsHP.text = "" + playerStats[selectedChar].currentHP + "/" + playerStats[selectedChar].maxHP;
        statsMP.text = "" + playerStats[selectedChar].currentMP + "/" + playerStats[selectedChar].maxMP;
        StatsStrength.text = playerStats[selectedChar].STR.ToString();
        statsDefense.text = playerStats[selectedChar].DEF.ToString();
        statsEXPtoNextLvl.text = (playerStats[selectedChar].expToNextLvl[playerStats[selectedChar].playrtLvl]
                                   - playerStats[selectedChar].currentEXP).ToString();
        statsImage.sprite = playerStats[selectedChar].charImage;
    }

    public void ShowItems()
    {
        GameManager.instance.SortItems();
        for(int i = 0; i < itemButtons.Length; i++)
        {
            itemButtons[i].buttonValue = i;

            if(GameManager.instance.itemsHeld[i] != "")
            {
                itemButtons[i].itemImage.gameObject.SetActive(true);
                itemButtons[i].itemImage.sprite = GameManager.instance.GetItemDetails(GameManager.instance.itemsHeld[i]).itemSprite;
                itemButtons[i].amountText.text = GameManager.instance.itenNum[i].ToString();
            } else
            {
                itemButtons[i].itemImage.gameObject.SetActive(false);
                itemButtons[i].amountText.text = "";
            }
        }
    }
    public void SelectItem(Items newItem)
    {
        activeItem = newItem;

        if(activeItem.isItem == true)
        {
            useButtonText.text = "Use";
        }

        if(activeItem.isArmor || activeItem.isWeapon)
        {
            useButtonText.text = "Equip";
        }

        itemname.text = activeItem.itemName;
        itemDisc.text = activeItem.itemDisc;
    }

    public void DiscardItem()
    {
        if(activeItem != null)
        {
            GameManager.instance.RemoveItem(activeItem.itemName);
        }
    }

    public void OpenItemCharChoice()
    {
        itemCharChiceMenu.SetActive(true);

        for(int i = 0; i < itemCharChoiceNames.Length; i++)
        {
            itemCharChoiceNames[i].text = GameManager.instance.playerStats[i].charName;
            itemCharChoiceNames[i].transform.parent.gameObject.SetActive(GameManager.instance.playerStats[i].gameObject.activeInHierarchy);
        }
    }

    public void CloseItemCharChoice()
    {
        itemCharChiceMenu.SetActive(false);
    }
    public void UseItem(int selectChar)
    {
        activeItem.Use(selectChar);
        CloseItemCharChoice();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
        GameMenu.instance.CloseMenu();
    }

    public void LevelSelect()
    {
        SceneManager.LoadScene("LevelSelect");
        Time.timeScale = 1f;
        GameMenu.instance.CloseMenu();
    }
}
