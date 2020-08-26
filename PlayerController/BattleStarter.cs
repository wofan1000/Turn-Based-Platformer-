using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStarter : MonoBehaviour
{
    public BattleType[] battles;

    private bool inArea;

    public bool activateBattleOnEnter;

    public bool deactivateAfterStart;

    public bool cannotFlee;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            activateBattleOnEnter = true;
            StartCoroutine(StartBattleCo());
            PlayerController.canMove = false;
        } 
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            activateBattleOnEnter = false;
            
        }

    }


    public IEnumerator StartBattleCo()
    {
        GameManager.instance.battleActive = true;

        int selectedbttle = Random.Range(0,battles.Length);

        BattleManager.instance.rewardItems = battles[selectedbttle].rewardItems;
       BattleManager.instance.rewardExp = battles[selectedbttle].rewardExp;

        yield return new WaitForSeconds(1.5f);

        BattleManager.instance.BattleStart(battles[selectedbttle].enemies, cannotFlee);

        if(deactivateAfterStart)
        {
            gameObject.SetActive(false);
        }

    }

}
