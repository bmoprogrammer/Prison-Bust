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
        // Have audio clip play when coin is picked up and destroy the game object.
        if (other.tag == "Player" && !wasCollected)
        {
            wasCollected = true;
            FindObjectOfType<GameSession>().addToScore(pointsForPickup);
            AudioSource.PlayClipAtPoint(coinPickupSFX, Camera.main.transform.position, 0.1f);
            Destroy(gameObject);
        }
    }
}
