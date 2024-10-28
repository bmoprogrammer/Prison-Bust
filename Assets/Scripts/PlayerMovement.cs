using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 10f;
    Vector2 moveInput;
    Rigidbody2D myRigidbody;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        Run();
        FlipSprite();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void Run()
    {
        // playerVelocity will creat a new Vector2 struct that multiplies the input on the x axis by 10
        // then grab the velocity of the y axis. It doesn't matter in this case since you can't run vertically
        Vector2 playerVelocity = new(moveInput.x * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;
    }

    void FlipSprite()
    {
        // Check to see if the player has any movement greater than epsilon (slightly above 0)
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

        // If the player has horizontal movement then flip the X scale value which flips the sprite
        if(playerHasHorizontalSpeed)
        {
            // Mathf.Sign will return a value of 1 when f is positive or 0, or return -1 when negative
            // In this case, have the value of the scale be either 1 or -1 based on the input 
            // The player input will return -1 when left is pressed and 1 when right is pressed
            transform.localScale = new Vector2 (Mathf.Sign(myRigidbody.velocity.x), 1f);
        }
    }
}
