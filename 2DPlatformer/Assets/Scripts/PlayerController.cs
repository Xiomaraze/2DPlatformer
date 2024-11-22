using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum FacingDirection
    {
        left, right
    }

    //a d is left right
    Rigidbody2D rb;
    public float Speed;
    public float jumpHeight;
    Vector2 leftRight;
    Vector2 jump = Vector2.zero;
    Vector2 jumpStart;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
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
        //houston we have reached mach 2 (i had velocity set to += instead of just equals)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpStart = rb.position;
            jump = Vector2.up;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            jump = Vector2.zero;
            jumpStart = Vector2.zero;
        }
        if (jump == Vector2.up && (rb.position.y >= (jumpStart.y + jumpHeight)))
        {
            jump = Vector2.zero;
        }
        jump = jump * Speed / 100 * Time.fixedDeltaTime;
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
        playerInput = leftRight + jump;
        MovementUpdate(playerInput);
    }

    private void MovementUpdate(Vector2 playerInput)
    {
        rb.velocity = playerInput;
    }

    public bool IsWalking()
    {
        return false;
    }
    public bool IsGrounded()
    {
        return true;
    }

    public FacingDirection GetFacingDirection()
    {
        return FacingDirection.left;
    }
}
