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

        // If there are more than one game session objects then destroy them.
        if (numGameSessions > 1)
            Destroy(gameObject);
        else
            DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        livesText.text = playerLives.ToString();
        scoreText.text = score.ToString();
    }

    public void AddToScore(int pointsGained)
    {
        score += pointsGained;
        scoreText.text = score.ToString();
    }

    public void ProcessPlayerDeath()
    {
        if (playerLives > 1)
            StartCoroutine(DeathPause());
        else
            ResetGameSession();
    }

    private void ResetGameSession()
    {
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    IEnumerator DeathPause()
    {
        yield return new WaitForSecondsRealtime(respawnTimer);

        playerLives--;
        livesText.text = playerLives.ToString();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
