using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    Rigidbody2D myRigidbody;
    BoxCollider2D myPeriscope;
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myPeriscope = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        // Set enemy speed
        myRigidbody.velocity = new Vector2(moveSpeed, 0f);
    }

    // When the enemy runs into a wall then have the enemy go the other way and flip the sprite
    private void OnTriggerExit2D(Collider2D other) {
        moveSpeed = -moveSpeed;
        FlipEnemy();
    }

    void FlipEnemy()
    {
        // Since this is only called when the enemy runs into a wall, just negate the x scale value.
        transform.localScale = new Vector2(-transform.localScale.x, 1f);
    }
}
