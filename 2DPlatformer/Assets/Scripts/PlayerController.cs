using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum FacingDirection
    {
        left, right
    }

    BoxCollider2D boxCollider;
    public bool contact;
    //a d is left right
    Rigidbody2D rb;
    public float Speed;
    Vector2 leftRight;
    Vector2 jumpStart;
    Vector2 jumpTakeoff = Vector2.zero;
    Vector2 jumpMax;
    public GameObject testSquare;
    Rigidbody2D testDimention;
    float gravityStore;

    public float apexHeight;
    public float apexTime;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = rb.GetComponent<BoxCollider2D>();
        testDimention = testSquare.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        testDimention.velocity = Vector2.up;
        //The input from the player needs to be determined and then passed in the to the MovementUpdate which should
        //manage the actual movement of the character.
        Vector2 playerInput = new Vector2();

        if (Input.anyKey)
        {
            if (Input.GetKey(KeyCode.A))
            {
                leftRight = Vector2.left;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                leftRight = Vector2.right;
            }
        }
        else
        {
            leftRight = Vector2.zero;
        }
        leftRight = leftRight * Speed * Time.fixedDeltaTime;

        if (Input.GetKey(KeyCode.Space))
        {
            if (contact)
            {
                jumpStart = rb.transform.position;
                jumpMax = new Vector2(jumpStart.x, jumpStart.y + apexHeight);
                Debug.Log("Start at " + jumpStart + " End at " +  jumpMax);
                gravityStore = rb.gravityScale;
                rb.gravityScale = 0;
                jumpTakeoff = Vector2.up;
                float jumpSpeed;
                //jumpSpeed = jumpTakeoff.y * apexHeight;
                //jumpSpeed = jumpSpeed / apexTime;
                //jumpSpeed *= Time.fixedDeltaTime;
                //jumpTakeoff.y = jumpSpeed;
            }
            else
            {
            }
        }

        if (!contact)
        {
            if (rb.velocity.y > 0)
            {
                if (rb.position.y >= jumpMax.y)
                {
                    rb.gravityScale = 1f;
                }
            }
        }

        //week 10 jumping code below week 11 in new method
        ////houston we have reached mach 2 (i had velocity set to += instead of just equals)
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    jumpStart = rb.position;
        //    jump = Vector2.up;
        //}
        //else if (Input.GetKeyUp(KeyCode.Space))
        //{
        //    jump = Vector2.zero;
        //    jumpStart = Vector2.zero;
        //}
        //if (jump == Vector2.up && (rb.position.y >= (jumpStart.y + jumpHeight)))
        //{
        //    jump = Vector2.zero;
        //}
        //jump = jump * Speed / 100 * Time.fixedDeltaTime;
        //the below code ended up with a hilarious "inching forwards" type of effect
        //if (Input.GetKeyDown(KeyCode.A)) //moving left
        //{
        //    playerInput = Vector2.left;
        //}
        //else if (Input.GetKeyDown(KeyCode.D))
        //{
        //    playerInput = Vector2.right;
        //}
        //else if (Input.anyKeyDown != true)
        //{
        //    //stop
        //    playerInput = Vector2.zero;
        //}
        //playerInput = playerInput * Speed * Time.fixedDeltaTime;
        //and now we put the jump and leftright together
        playerInput = leftRight + jumpTakeoff;
        MovementUpdate(playerInput);
    }

    private void MovementUpdate(Vector2 playerInput)
    {
        rb.velocity = playerInput;
    }

    public bool IsWalking()
    {
        if (rb.velocity.x != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //this section is "Are ya touching the ground!???"
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            contact = true;
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            contact = false;
        }
    }

    // this section is "well heres how you fall"
    public bool IsGrounded()
    {
        if ((rb.velocity.y != 0) || !contact)
        {
            return false;
        }
        else
        {
            return true;
        }
        
    }

    public FacingDirection GetFacingDirection()
    {
        if (rb.velocity.x > 0)
        {
            return FacingDirection.right;
        }
        else
        {
            return FacingDirection.left;
        }
    }
}
