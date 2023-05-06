using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
    [SerializeField]
    private int _difficulty;

    private Button _difficultyButton;
    private GameManager _gameManager;

    void Start()
    {
        _difficultyButton = GetComponent<Button>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        _difficultyButton.onClick.AddListener(SetDifficulty);
    }

    
    void Update()
    {
        
    }

    void SetDifficulty()
    {
        _gameManager.StartGame(_difficulty);
    }
}
