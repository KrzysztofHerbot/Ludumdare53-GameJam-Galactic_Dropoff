using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scorekeeper : MonoBehaviour
{
    private static Scorekeeper instance;

    // this is a singleton; 
    private void Awake()
    {
        // if another instance of this class ever tries to wake up, destroy it.
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // We are the one and only instance; never destroy.
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public int score = 0;
    public int maxScore = 0;
    public void Reset()
    {
        score = 0;
        maxScore = 0;
    }

    public int GetScore() { return score; }
    public int GetMaxScore() { return maxScore; }
    public void AddStars(int starCount) { 
        score += starCount;
        maxScore += 3;
    }

    // We can call the static method "Scorekeeper.GetInstance()" from anywhere and get a reference to this.
    public static Scorekeeper GetInstance()
    {
        if (instance == null)
        {
            GameObject scorekeeperObj = new GameObject("Scorekeeper");
            instance = scorekeeperObj.AddComponent<Scorekeeper>();
            DontDestroyOnLoad(scorekeeperObj);
        }
        return instance;
    }
}