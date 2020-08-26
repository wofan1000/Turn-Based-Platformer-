using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed;

    public Transform leftPos, rightPos;

    private   bool moveRight;

    private Rigidbody2D theRB;

    public SpriteRenderer theSR;
    // Start is called before the first frame update
    void Start()
    {
        theRB = GetComponent<Rigidbody2D>();

        leftPos.parent = null;

        rightPos.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        if(moveRight)
        {
            theRB.velocity = new Vector2(moveSpeed, theRB.velocity.y);

            theSR.flipX = true;

            if(transform.position.x > rightPos.position.x)
            {
                moveRight = false;
            } else
            {
                theRB.velocity = new Vector2(-moveSpeed, theRB.velocity.y);

                theSR.flipX = true;

                if (transform.position.x < leftPos.position.x)
                {
                    moveRight = true;
                }
                
            }
        }
    }
}
