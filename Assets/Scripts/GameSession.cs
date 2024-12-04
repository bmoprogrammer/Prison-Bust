using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] int RespawnTimer = 1; 

    // Awake is the very first thing to happen when the game starts
    void Awake()
    {
        // Using FindObjectsOfType to find multiple instances of an object instead of just the first
        int numGameSessions = FindObjectsOfType<GameSession>().Length;

        // If there are more than one game session objects then destroy them.
        if (numGameSessions > 1)
            Destroy(gameObject);
        else
            DontDestroyOnLoad(gameObject);
    }

    
    public void ProcessPlayerDeath()
    {
        // If player 
        if (playerLives > 1)
        {
            StartCoroutine(DeathPause());
            TakeLife();
        }
        else
            ResetGameSession();
    }

    private void TakeLife()
    {
        playerLives--;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void ResetGameSession()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    IEnumerator DeathPause()
    {
        yield return new WaitForSecondsRealtime(RespawnTimer);
    }
}
