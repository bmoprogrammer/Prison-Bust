using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 20f;
    float xSpeed;
    Rigidbody2D myRigidbody;
    PlayerMovement player;
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
        xSpeed = player.transform.localScale.x * bulletSpeed;
    }

    void Update()
    {
        myRigidbody.velocity = new Vector2 (xSpeed, 0f);
    }
    
    private void OnTriggerEnter2D(Collider2D other) 
    {
        // If bullet hits an enemy then destroy the enemy object then destroy bullet object.
        if(other.gameObject.tag == "Enemy")
        {
            Destroy(other.gameObject);
        }
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        // If bullet hits wall then destroy bullet object.
        Destroy(gameObject);
    }
}
