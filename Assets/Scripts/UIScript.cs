using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    private int _currentScore = 0;
    private Text _scoreText;

    // Start is called before the first frame update
    void Start()
    {
        //set score value to 0
        _scoreText = GameObject.Find("Score").GetComponent<Text>();
        _scoreText.text += _currentScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore(int score)
    {
        //replace old score value to a new one
        _scoreText.text = _scoreText.text.Replace(_currentScore.ToString(), (_currentScore + score).ToString());
        _currentScore += score;
    }

    public void ShowGameOverMessage()
    {
        //if game is over, enable Gamve Over message
        GameObject.Find("GameOver").GetComponent<Text>().enabled = true;
    }
}
