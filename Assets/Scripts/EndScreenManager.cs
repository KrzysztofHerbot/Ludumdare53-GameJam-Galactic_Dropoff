using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreenManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            // reset the score.
            Scorekeeper scorekeeper = Scorekeeper.GetInstance();
            if (scorekeeper != null) scorekeeper.Reset();

            SceneManager.LoadScene(1); // Load the first scene
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex + 1); // Load the next scene
        }
    }
}
