using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 23f;
    [SerializeField] float climbSpeed = 7.5f;
    float playerGravity;
    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    CapsuleCollider2D myCollider;

    void Start()
    {
        // Cache references to the Rigidbody2D, Animator, CapsuleCollider2D objects, and gravity.
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCollider = GetComponent<CapsuleCollider2D>();
        playerGravity = myRigidbody.gravityScale;
    }

    
    void Update()
    {
        Run();
        FlipSprite();
        ClimbLadder();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if(!myCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))){return;}
        if(value.isPressed)
        {
            // Add vertical jump speed velocity to the player when space bar is pressed.
            myRigidbody.velocity += new Vector2(0f, jumpSpeed);
        }
    }

    void Run()
    {
        /*  playerVelocity will create a new Vector2 struct that multiplies the input on the x axis by 10
            then grab the velocity of the y axis. It doesn't matter in this case since you can't run vertically */
        Vector2 playerVelocity = new(moveInput.x * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;

        // Check to see if player is running. If they are, set running animation to true. Otherwise idle.
        if(Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon)
        {
            myAnimator.SetBool("isRunning", true);
        } else {
            myAnimator.SetBool("isRunning", false);
        }
    }

    void FlipSprite()
    {
        // Check to see if the player has any movement greater than epsilon (slightly above 0)
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

        // If the player has horizontal movement then flip the X scale value which flips the sprite
        if(playerHasHorizontalSpeed)
        {
            /*  Mathf.Sign will return a value of 1 when f is positive or 0, or return -1 when negative
                In this case, have the value of the scale be either 1 or -1 based on the input 
                The player input will return -1 when left is pressed and 1 when right is pressed */
            transform.localScale = new Vector2 (Mathf.Sign(myRigidbody.velocity.x), 1f);
        }
    }

    void ClimbLadder()
    {
        /*  If the player is not touching the ladder layer then set the player gravity back to normal 
            set the climbing animation to false and return from the function. */
        if (!myCollider.IsTouchingLayers(LayerMask.GetMask("Ladder"))) 
        { 
            myRigidbody.gravityScale = playerGravity;
            myAnimator.SetBool("isClimbing", false);
            return;
        }
        
        /*  If the player is touching the ladder layer then set the player vertical velocity to 
            equal the climb velocity, set the player gravity to 0 to prevent slowly falling while on
            the ladder, then set climbing animation to true if vertical speed is greater than epsilon.*/
        Vector2 climbVelocity = new Vector2 (myRigidbody.velocity.x, moveInput.y * climbSpeed);
        myRigidbody.velocity = climbVelocity;
        myRigidbody.gravityScale = 0f;

        bool playerHasVerticalSpeed = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("isClimbing", playerHasVerticalSpeed);
    }
}
