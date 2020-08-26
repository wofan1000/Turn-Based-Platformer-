using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public float moveSpeed;
    public Rigidbody2D theRb;
    public float jumpForce;

    public bool isGround;
    public static bool canMove;
    public bool stopInput;

    public Transform groundCheckPoint;
    public LayerMask whatGround;

    private bool candoubleJump;

    private Animator anim;
    private SpriteRenderer theSr;

    public float knockbackLenth, knockbackForce;
    private float knockbackCounter;

    // Start is called before the first frame update
    
    void Start()
    {
        
        anim = GetComponent<Animator>();
        theSr = GetComponent<SpriteRenderer>();
        instance = this;
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove && !stopInput)
        {
            if (knockbackCounter <= 0)
            {


                theRb.velocity = new Vector2(moveSpeed * Input.GetAxisRaw("Horizontal"), theRb.velocity.y);

                isGround = Physics2D.OverlapCircle(groundCheckPoint.position, 0.2f, whatGround);


                if (isGround)
                {
                    candoubleJump = true;
                }
                if (Input.GetButtonDown("Jump"))
                {
                    if (!isGround)
                    {
                        theRb.velocity = new Vector2(theRb.velocity.x, jumpForce);
                    }
                    else
                    {
                        if (candoubleJump)
                        {
                            theRb.velocity = new Vector2(theRb.velocity.x, jumpForce);
                            candoubleJump = false;
                        }
                    }
                }
                if (theRb.velocity.x < 0)
                {
                    theSr.flipX = true;
                }
                else if (theRb.velocity.x > 0)
                {
                    theSr.flipX = false;
                }
            } else
            {
                knockbackCounter -= Time.deltaTime;

                if (!theSr.flipX)
                {
                    theRb.velocity = new Vector2(-knockbackForce, theRb.velocity.y);
                }
                else
                {
                    theRb.velocity = new Vector2(knockbackForce, theRb.velocity.y);
                }
                anim.SetFloat("MoveSpeed", Mathf.Abs(theRb.velocity.x));
                anim.SetBool("IsGround", isGround);
            }
        }
    }


                public void Knockback()
                {
                    knockbackCounter = knockbackLenth;
                    theRb.velocity = new Vector2(0f, knockbackForce);
                }
            }

        
    

