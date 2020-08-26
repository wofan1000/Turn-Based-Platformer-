using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BattleManager : MonoBehaviour
{
    public  static BattleManager instance;

    private bool battleActive;
  

    public GameObject battleScene;

    public Transform[] playerPos, enemyPos;
    public BattleChar[] playerPrefabs, enemyPrefabs;

    public List<BattleChar> activeBattlers = new List<BattleChar>();

    public int currentTurn;
    public bool waitTurn;

    public GameObject uiButtonsHolder;

    public BattleMove[] moveList;

    public GameObject attackParticleEffect;

    public DmgNumber dmgNumner;

    public Text[] playerName, playerHP, playerMP;

    public GameObject targetMenu;
    public BattleTargetButton[] targetButtons;
    public GameObject magicMenu, itemsMenu;
    public BattleMagic[] magicButtons;

    public BattleNotification BattleNotice;

    public string[] theItems;

    public BattleItemSelect[] itemButtons;

    public Items activeItem;

    public Text itemName, itemDescription, useButtonText;
    public GameObject itemCharChoiceMenu;
    public Text[] itemCharChoiceNames;

    public CharStats[] playerStats;  //this is to reference GameManager's stat


    public int fleeChance = 25;
    private bool fleeing;
    public bool cannotFlee;

    public int rewardExp;
    public string[] rewardItems;

    // Start is called before the first frame update
    
    void Start()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
       
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void BattleStart(string[] enemiesToSpawn, bool cannotFlee)
    {
        if (!battleActive)
        {
            battleActive = true;

            GameManager.instance.battleActive = true;

            transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y);
            battleScene.SetActive(true);
            PlayerController.canMove = false;

            for (int i = 0; i < playerPos.Length; i++)
            {
                if (GameManager.instance.playerStats[i].gameObject.activeInHierarchy)
                {
                    for (int j = 0; j < playerPrefabs.Length; j++)
                    {
                        if (playerPrefabs[j].CharName == GameManager.instance.playerStats[i].charName)
                        {
                            BattleChar newPlayer = Instantiate(playerPrefabs[j], playerPos[i].position, playerPos[i].rotation);
                            newPlayer.transform.parent = playerPos[i];
                            activeBattlers.Add(newPlayer);

                            CharStats theplayer = GameManager.instance.playerStats[i];
                            activeBattlers[i].currentHP = theplayer.currentHP;
                            activeBattlers[i].MaxHP = theplayer.maxHP;
                            activeBattlers[i].MaxMP = theplayer.maxMP;
                            activeBattlers[i].currentMP = theplayer.currentMP;
                            activeBattlers[i].wpnPWR = theplayer.wpnPwr;
                            activeBattlers[i].ArmorPWR = theplayer.armorPWr;
                            activeBattlers[i].defense = theplayer.DEF;
                            activeBattlers[i].strength = theplayer.STR;

                        }
                    }


                }
            }

            for (int i = 0; i < enemiesToSpawn.Length; i++)
            {
                if (enemiesToSpawn[i] != "")
                {
                    for (int j = 0; j < enemyPrefabs.Length; j++)
                    {
                        if (enemyPrefabs[j].CharName == enemiesToSpawn[i]) {
                            BattleChar newenemy = Instantiate(enemyPrefabs[j], enemyPos[i].position, enemyPos[i].rotation);
                            newenemy.transform.parent = enemyPos[i];
                            activeBattlers.Add(newenemy);
                        }
                    }
                }
            }

            waitTurn = true;
            currentTurn = Random.Range(0, activeBattlers.Count);
            UpdateStats();
            UpdateBattle();
        }
    }

    public void NextTurn()
    {
        currentTurn++;
        if (currentTurn >= activeBattlers.Count)
        {
            currentTurn = 0;
        }
        waitTurn = true;
        UpdateBattle();
        UpdateStats();
    }

    public void UpdateBattle()
    {
        bool allEnemiesDead = true;
        bool allPlayersDead = true;

        for (int i = 0; i < activeBattlers.Count; i++)
        {
            if (activeBattlers[i].currentHP < 0)
            {
                activeBattlers[i].currentHP = 0;
            }

            if (activeBattlers[i].currentHP == 0)
            {
                // handle dead bois//
                if(activeBattlers[i].isPlayer)
                {
                    activeBattlers[i].theSprite.sprite = activeBattlers[i].deadSprite;
                } else
                {
                    activeBattlers[i].EnemyFade();
                }
            } else

            {
                if (activeBattlers[i].isPlayer)
                {
                    allPlayersDead = false;
                    activeBattlers[i].theSprite.sprite = activeBattlers[i].aliveSprite;
                } else
                {
                    allEnemiesDead = false;
                }
            }
        }

        if (allEnemiesDead || allPlayersDead)
        {
            if (allEnemiesDead)
            {
               
                StartCoroutine(EndBattleCo());
            } else
            {
                Debug.Log("All Players Dead");
                StartCoroutine(GameOverCo());
            }
        
        battleScene.SetActive(false);
        GameManager.instance.battleActive = false;
        battleActive = false;
    } else
    {
         while (activeBattlers[currentTurn].currentHP == 0)
            {
                currentTurn++;
                if(currentTurn >= activeBattlers.Count)
                {
                    currentTurn = 0;
                }
            }
    }
}

    public IEnumerator EnemyMoveCo()
    {
        waitTurn = false;
        yield return new WaitForSeconds(1f);
        EnemyAttack();
        yield return new WaitForSeconds(1f);
        NextTurn();
    }

    public void EnemyAttack()
    {
        List<int> Players = new List<int>();
        for(int i = 0; i < activeBattlers.Count; i++)
        {
            if(activeBattlers[i].isPlayer && activeBattlers[i].currentHP > 0)
            {
                Players.Add(i);
            }
        }
        int selectedTar = Players[Random.Range(0, Players.Count)];
        int selectAttack = Random.Range(0, activeBattlers[currentTurn].movesAvalible.Length);
        int MovePwr = 0;
        //activeBattlers[selectedTar].currentHP -= 20;

        
        for (int i = 0; i < moveList.Length; i++)
        {
            Instantiate(moveList[i].theEffect, activeBattlers[selectedTar].transform.position, activeBattlers[selectedTar].transform.rotation);
            MovePwr = moveList[i].movePwr;
        }
        Instantiate(attackParticleEffect, activeBattlers[currentTurn].transform.position, activeBattlers[currentTurn].transform.rotation);
        DealDmg(selectedTar, MovePwr);

    }

    public void DealDmg(int tar, int movePwr)
    {
        float attackPwr = activeBattlers[currentTurn].strength + activeBattlers[currentTurn].wpnPWR;
        float defPwr = activeBattlers[tar].defense + activeBattlers[tar].ArmorPWR;

        float dmgCalc = (attackPwr / defPwr) * movePwr * Random.Range(.9f, 1.2f);
        int dmgToGive = Mathf.RoundToInt(dmgCalc);

        if (activeBattlers[tar].isPlayer)

        {

            CharStats selectedChar = GameManager.instance.playerStats[tar];

            selectedChar.currentHP -= dmgToGive;

        }



        if (!activeBattlers[tar].isPlayer)

        {

            activeBattlers[tar].currentHP -= dmgToGive;

        }
        Instantiate(dmgNumner, activeBattlers[tar].transform.position, activeBattlers[tar].transform.rotation).setDmg(dmgToGive);
        UpdateStats();
    }

    public void UpdateStats()
    {
        for(int i = 0; i < playerName.Length; i++)
        {
            if (activeBattlers.Count > 1)
            {
                if (activeBattlers[i].isPlayer)
                {
                    BattleChar playerData = activeBattlers[i];

                    playerName[i].gameObject.SetActive(true);
                    playerName[i].text = playerData.CharName;
                    playerHP[i].text =Mathf.Clamp(playerData.currentHP, 0, int.MaxValue) + "/" + playerData.MaxHP;
                    playerMP[i].text = Mathf.Clamp(playerData.currentMP, 0, int.MaxValue) + "/" + playerData.MaxMP;


                }else
                {
                    playerName[i].gameObject.SetActive(false);
                    
                }
            }else
            {
                playerName[i].gameObject.SetActive(false);
            }
        }
    }

    public void PlayerAttack(string moveName ,int selectedTarget)
    {
        int MovePwr = 0;
        
        //activeBattlers[selectedTar].currentHP -= 20;


        for (int i = 0; i < moveList.Length; i++)
        {
            if (moveList[i].moveName == moveName)
            {
                Instantiate(moveList[i].theEffect, activeBattlers[selectedTarget].transform.position, activeBattlers[selectedTarget].transform.rotation);
                MovePwr = moveList[i].movePwr;
            }
        }
        Instantiate(attackParticleEffect, activeBattlers[currentTurn].transform.position, activeBattlers[currentTurn].transform.rotation);

        DealDmg(selectedTarget, MovePwr);

        uiButtonsHolder.SetActive(false);
        targetMenu.SetActive(false);

        NextTurn();
}
    public void OpenTargetMenu(string moveName)
    {
        targetMenu.SetActive(true);

        List<int> enemies = new List<int>();
        for(int i = 0; i < activeBattlers.Count; i++)
        {
            if(!activeBattlers[i].isPlayer)
            {
                enemies.Add(i);
            }
        }

        for(int i = 0; i < targetButtons.Length; i++)
        {
            if (enemies.Count > i && activeBattlers[enemies[i]].currentHP > 0)
            {
                targetButtons[i].gameObject.SetActive(true);
                targetButtons[i].moveName = moveName;
                targetButtons[i].battleTarget = enemies[i];
                targetButtons[i].target.text = activeBattlers[enemies[i]].CharName;
            } else
            {
                targetButtons[i].gameObject.SetActive(false);
            }
        }
    }

    public void OpenMagicMenu()
    {
        magicMenu.SetActive(true);
        for(int i =0; i < magicButtons.Length; i++)
        {
            if(activeBattlers[currentTurn].movesAvalible.Length > i)
            {
                magicButtons[i].gameObject.SetActive(true);

                magicButtons[i].spellName = activeBattlers[currentTurn].movesAvalible[i];
                magicButtons[i].nameText.text = magicButtons[i].spellName;

                for(int j = 0; j < moveList.Length; j++)
                {
                    if(moveList[j].moveName == magicButtons[i].spellName)
                    {
                        magicButtons[i].spellCost = moveList[i].moveCost;
                        magicButtons[i].costText.text = magicButtons[i].spellCost.ToString();
                    }
                }
            } else
            {
                magicButtons[i].gameObject.SetActive(true);
            }
        }
    }

    public void Flee()
    {
        if (cannotFlee)
        {
            BattleNotice.theText.text = "Couldnt Flee this Battle";
        }
        else
        {
            int fleeSucsess = Random.Range(0, 100);
            if (fleeSucsess < fleeChance)
            {
                //battleActive = false;
                //battleScene.SetActive(false);
                fleeing = true;
                StartCoroutine(EndBattleCo());
            }
            else
            {
                NextTurn();
                BattleNotice.theText.text = "Couldnt Flee";
                BattleNotice.Activate();
            }
        }


    }
    // code for battle items//
    public void ShowItem()

    {

        itemsMenu.SetActive(true);

        GameManager.instance.SortItems();



        for (int i = 0; i < itemButtons.Length; i++)

        {

            itemButtons[i].buttonValue = i;



            if (GameManager.instance.itemsHeld[i] != "")

            {

                itemButtons[i].buttonImage.gameObject.SetActive(true);

                itemButtons[i].buttonImage.sprite = GameManager.instance.GetItemDetails(GameManager.instance.itemsHeld[i]).itemSprite;

                itemButtons[i].amountText.text = GameManager.instance.itenNum[i].ToString();

            }

            else

            {

                itemButtons[i].buttonImage.gameObject.SetActive(false);

                itemButtons[i].amountText.text = "";



            }



        }

    }



    public void SelectItem(Items newItem)

    {

        activeItem = newItem;

        if (activeItem.isItem)

        {

            useButtonText.text = "Use";

        }



        if (activeItem.isWeapon || activeItem.isArmor)

        {

            useButtonText.text = "Equip";

        }



        itemName.text = activeItem.itemName;

        itemDescription.text = activeItem.itemDisc;
   }


    public void OpenItemCharChoice()

    {

        itemCharChoiceMenu.SetActive(true);

        for (int i = 0; i < itemCharChoiceNames.Length; i++)

        {

            itemCharChoiceNames[i].text = GameManager.instance.playerStats[i].charName;

            itemCharChoiceNames[i].transform.parent.gameObject.SetActive(GameManager.instance.playerStats[i].gameObject.activeInHierarchy);
          }
      }


    public void CloseItemCharChoice()

    {

        itemCharChoiceMenu.SetActive(false);

    }


    public void Useitem(int selectChar)

    {
      activeItem.Use(selectChar);

        UpdateStats();

        CloseItemCharChoice();

        itemsMenu.SetActive(false);

        NextTurn();

    }
    // end of code for battle items//

    public IEnumerator EndBattleCo()
    {
        battleActive = false;
        targetMenu.SetActive(false);
        magicMenu.SetActive(false);
        itemsMenu.SetActive(false);
        uiButtonsHolder.SetActive(false);

        yield return new WaitForSeconds(.5f);

        UIController.instance.FadeToBlack();


        for (int i = 0; i < activeBattlers.Count; i++)
        {
            if (activeBattlers[i].isPlayer)
            {
                for (int j = 0; j < GameManager.instance.playerStats.Length; j++)
                {
                    if (activeBattlers[i].CharName == GameManager.instance.playerStats[j].charName)
                    {
                        GameManager.instance.playerStats[j].currentHP = activeBattlers[i].currentHP;
                        GameManager.instance.playerStats[j].currentMP = activeBattlers[i].currentMP;
                    }
                }
            }
            Destroy(activeBattlers[i].gameObject);
        }
        UIController.instance.FadeFromBlack();
        battleScene.SetActive(false);
        activeBattlers.Clear();
        currentTurn = 0;
        if(fleeing)
        {
            GameManager.instance.battleActive = false;
            fleeing = false;
        } else
        {
            BattleRewards.instance.OpenRewardScreen(rewardExp, rewardItems);
        }
    }

    public IEnumerator GameOverCo()
    {
        battleActive = false;
        UIController.instance.FadeToBlack();
        yield return new WaitForSeconds(1.5f);
        battleScene.SetActive(false);
    

    }
}
