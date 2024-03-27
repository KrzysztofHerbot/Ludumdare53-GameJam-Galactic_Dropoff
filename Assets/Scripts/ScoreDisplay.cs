using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int score = 0;
        int maxScore = 0;
        
        Scorekeeper scorekeeper = Scorekeeper.GetInstance();
        if (scorekeeper != null) {
            score = scorekeeper.GetScore();
            maxScore = scorekeeper.GetMaxScore();
        }

        GetComponent<TextMeshPro>().text = "You received "+score.ToString()+" out of "+maxScore.ToString()+" stars.";
    }
}
