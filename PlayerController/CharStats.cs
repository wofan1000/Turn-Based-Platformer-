using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharStats : MonoBehaviour
{
    public string charName;
    public int playrtLvl = 1;
    public int currentEXP;
    public int[] expToNextLvl;
    public int maxLvl = 100;
    public int baseExp = 1000;
    public int currentHP, maxHP, currentMP, maxMP;
    public int STR, DEF, wpnPwr, armorPWr;
    public int[] MPBounus;
    public string eqdWpn, eqdArmr;

    public Sprite charImage;

   

    // Start is called before the first frame update
    void Start()
    {
        expToNextLvl = new int[maxLvl];
        expToNextLvl[1] = baseExp;

        for(int i = 2; i < expToNextLvl.Length; i++)
        {
            expToNextLvl[i] = Mathf.FloorToInt(expToNextLvl[i - 1] * 1.05f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            addEXP(500);
        }
    }

    public void addEXP(int expToAdd)
    {
        currentEXP += expToAdd;

        if (playrtLvl < maxLvl)
        {
            if (currentEXP > expToNextLvl[playrtLvl])
            {
                currentEXP -= expToNextLvl[playrtLvl];
                playrtLvl++;

                // add to srtrength or defense
                if (playrtLvl % 2 == 0)
                {
                    STR++;
                }
                else
                {
                    DEF++;
                }

                maxHP = Mathf.FloorToInt(maxHP * 1.05f);
                currentHP = maxHP;

                maxMP = maxMP * MPBounus[playrtLvl];
                currentMP = maxMP;
            }
        }

        if(playrtLvl >= maxLvl)
        {
            currentEXP = 9999/9999;
        }
    }
}
