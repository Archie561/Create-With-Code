using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    int lives = 3;
    int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        AddLives(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddLives(int livesAmount)
    {
        lives += livesAmount;
        if (lives <= 0)
        {
            Debug.Log("Game Over!");
        }
        else
        {
            Debug.Log("Lives: " + lives);
        }
    }

    public void AddScores(int scoresAmount)
    {
        score += scoresAmount;
        Debug.Log("Score: " + score);
    }
}
