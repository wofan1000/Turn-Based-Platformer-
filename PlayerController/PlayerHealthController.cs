using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;
    public int currentHealth, maxHealth;
    public float inviLength;
    private float inviCounter;
    private SpriteRenderer theSR;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        theSR = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if(inviCounter > 0)
        {
            inviCounter -= Time.deltaTime;
            if(inviCounter <= 0)
            {
                theSR.color = new Color(theSR.color.r, theSR.color.g, theSR.color.b, 1f);
            }
        }
    }
    // Update is called once per frame
    public  void DealDmg()
    {
        
        if (inviCounter <= 0)
        {
            currentHealth -= 5;

            if (currentHealth <= 0)
            {
                //gameObject.SetActive(false);

                LevelManager.instance.respawnPlayer();
            }
            else
            {
                inviCounter = inviLength;
                theSR.color = new Color(theSR.color.r, theSR.color.g, theSR.color.b, .5f);
                PlayerController.instance.Knockback();
            }
        }
    }
}
