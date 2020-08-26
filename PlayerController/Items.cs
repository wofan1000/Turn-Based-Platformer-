using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    public string itemName, itemDisc;
    public int value;
    public bool isArmor, isItem, isWeapon;
    public Sprite itemSprite;

    public int amoutToChange;
    public bool afectHP, afectMP, affectstr, affectDef;

    public int armorStr, weponStr;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Use(int charToUse)
    {
        CharStats selectedChar = GameManager.instance.playerStats[charToUse];

        if (isItem)
        {
            if (afectHP)
            {
                selectedChar.currentHP += amoutToChange;

                if (selectedChar.currentHP > selectedChar.maxHP)
                {
                    selectedChar.currentHP = selectedChar.maxHP;
                }
            }
            if (afectMP)
            {
                selectedChar.currentHP += amoutToChange;

                if (selectedChar.currentMP > selectedChar.maxMP)
                {
                    selectedChar.currentMP = selectedChar.maxMP;
                }
            }
            if (affectstr)
            {
                selectedChar.STR += amoutToChange;
            }
        }

        if(isWeapon)
        {
            if(selectedChar.eqdWpn != "")
            {
                GameManager.instance.AddItem(selectedChar.eqdWpn);
            }

            selectedChar.eqdWpn = itemName;
            selectedChar.wpnPwr = armorStr;
        }
        if (isArmor)
        {
            if (selectedChar.eqdArmr != "")
            {
                GameManager.instance.AddItem(selectedChar.eqdArmr);
            }

            selectedChar.eqdArmr = itemName;
            selectedChar.wpnPwr = armorStr;
        }

        GameManager.instance.RemoveItem(itemName);
    }
}
