using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] int score;
    [SerializeField] float respawnTimer = 1f; 
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;

    // Awake is the very first thing to happen when the game starts
    void Awake()
    {
        // Using FindObjectsOfType to find multiple instances of an object instead of just the first
        int numGameSessions = FindObjectsOfType<GameSession>().Length;

        // If there are more than one game session objects then destroy them. If not, keep the current session.
        if (numGameSessions > 1)
            Destroy(gameObject);
        else
            DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        // On start, convert the player lives and score integers to strings to display on UI.
        livesText.text = playerLives.ToString();
        scoreText.text = score.ToString();
    }

    // Function to add score to current score and updating value
    public void AddToScore(int pointsGained)
    {
        score += pointsGained;
        scoreText.text = score.ToString();
    }

    // When the player dies, if their live count is higher than 1 then start the death pause coroutine.
    // Otherwise reset the game session back to the beginning.
    public void ProcessPlayerDeath()
    {
        if (playerLives > 1)
            StartCoroutine(DeathPause());
        else
            ResetGameSession();
    }

    // Reset the scene persistence, load level 1, and destroy the current game session to create a new one.
    private void ResetGameSession()
    {
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    /*  Created this coroutine for the player to process they have died instead of death being instant.
        When the player does die, reduce player lives by 1, update the live counter on UI, and reload the
        current level. */
    IEnumerator DeathPause()
    {
        yield return new WaitForSecondsRealtime(respawnTimer);

        playerLives--;
        livesText.text = playerLives.ToString();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
