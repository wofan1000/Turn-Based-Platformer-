using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleRewards : MonoBehaviour
{

  public static  BattleRewards instance;

    public Text xpText, itemText;
    public GameObject rewardsScreen;

    public string[] rewarsedItems;
    public int xpEarned;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Y))
        {
            OpenRewardScreen(100, new string[] { "Iron Sword" , "Iron Armor" });
        }
    }

    public void OpenRewardScreen(int xp, string[] rewards)
    {
        xpEarned = xp;
        rewarsedItems = rewards;

        xpText.text = "Everyone Earned " + xpEarned + " Exp!";
        itemText.text = "";

        for(int i = 0; i < rewarsedItems.Length; i++)
        {
            itemText.text += rewards[i] + "\n";
        }

        rewardsScreen.SetActive(true);
    }

    public void CloseRewardScreen()
    {
        for(int i = 0; i < GameManager.instance.playerStats.Length; i++)
        {
            if(GameManager.instance.playerStats[i].gameObject.activeInHierarchy)
            {
                GameManager.instance.playerStats[i].addEXP(xpEarned);
            }
        }

        for(int i = 0; i <rewarsedItems.Length; i++)
        {
            GameManager.instance.AddItem(rewarsedItems[i]);
        }

        rewardsScreen.SetActive(false);
        GameManager.instance.battleActive = false;
    }
}
