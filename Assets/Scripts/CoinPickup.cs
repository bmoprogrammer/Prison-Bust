using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip coinPickupSFX;
    [SerializeField] int pointsForPickup = 100;  

    bool wasCollected = false;  
    private void OnTriggerEnter2D(Collider2D other) 
    {
        /*  If the player object interacts with the coin and is alive and the coin has not been collected,
            have audio clip play when coin is picked up, add points to score, and destroy the game object. */
        if (other.tag == "Player" && !wasCollected && FindObjectOfType<PlayerMovement>().isAlive)
        {
            wasCollected = true;
            FindObjectOfType<GameSession>().AddToScore(pointsForPickup);
            AudioSource.PlayClipAtPoint(coinPickupSFX, Camera.main.transform.position, 0.1f);
            Destroy(gameObject);
        }
    }
}
