using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProgressManager : MonoBehaviour
{
    public int initialAmmo = 8;
    
    private bool levelHasBeenBeat = false;
    
    // Start is called before the first frame update
    private PackageGoal[] packageGoals;
    void Start()
    {
        // Get a list of all goals
        packageGoals = FindObjectsOfType<PackageGoal>();
    }

    // Update is called once per frame
    void Update()
    {
        if (AllGoalsAreComplete()) {
            if (!levelHasBeenBeat) BeatLevel();
        }

        if (Input.GetKeyDown(KeyCode.R)) ResetLevel();
    }

    private bool AllGoalsAreComplete()
    {
        foreach (PackageGoal goal in packageGoals)
        {
            if (!goal.IsCompleted()) return false;
        }
        return true;
    }

    void BeatLevel()
    {
        levelHasBeenBeat = true;
        DisplayScore();

        FindObjectOfType<StarDisplayer>().AddStar();
        Invoke("GiveEasyStar", 0.5f);
        Invoke("GiveHardStar", 1.0f);
        Invoke("LoadNextLevel", 3.0f);
    }

    private bool HasAmmo(int threshold)
    {
        return FindObjectOfType<LaunchPackageOnClick>().ammo >= threshold;
    }

    private int GetAmmo()
    {
        return FindObjectOfType<LaunchPackageOnClick>().ammo;
    }

    void GiveEasyStar()
    {
        // Give star for having remaining some ammo
        if (HasAmmo(2))
        {
            FindObjectOfType<StarDisplayer>().AddStar();
            return;
        }

        // Give star if we only had enough ammo to complete the level.
        if (packageGoals.Length >= initialAmmo-2)
        {
            FindObjectOfType<StarDisplayer>().AddStar();
            return;
        }
    }

    void GiveHardStar()
    {
        // Give a star for only using one ammo per goal
        int ammoUsed = initialAmmo - GetAmmo();
        Debug.Log(packageGoals.Length);
        Debug.Log(ammoUsed);
        if (ammoUsed == packageGoals.Length)
        {
            FindObjectOfType<StarDisplayer>().AddStar();
        }
    }

    void ResetLevel()
    {
        // Reload the current scene
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    void DisplayScore()
    {
        Ship ship = FindObjectOfType<Ship>();
        Debug.Log("Your score was: " + ship.packagesLaunched);
    }

    void LoadNextLevel()
    {
        Scorekeeper scorekeeper = Scorekeeper.GetInstance();
        if (scorekeeper!=null) scorekeeper.AddStars(FindObjectOfType<StarDisplayer>().starsUnlocked);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1); // Load the next scene
    }
}
