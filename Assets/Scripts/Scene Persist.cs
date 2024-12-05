using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenePersist : MonoBehaviour
{
    void Awake()
    {
        // Using FindObjectsOfType to find multiple instances of an object instead of just the first
        int numScenePersists = FindObjectsOfType<ScenePersist>().Length;

        // If there are more than one game session objects then destroy them.
        if (numScenePersists > 1)
            Destroy(gameObject);
        else
            DontDestroyOnLoad(gameObject);
    }

    public void ResetScenePersist()
    {
        Destroy(gameObject);
    }
}
