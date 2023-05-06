using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _targets;
    [SerializeField]
    private TextMeshProUGUI _scoreText;
    [SerializeField]
    private TextMeshProUGUI _livesText;
    [SerializeField]
    private TextMeshProUGUI _gameOverText;
    [SerializeField]
    private Button _restartButton;
    [SerializeField]
    private AudioSource _backgroundMusic;
    [SerializeField]
    private Slider _soundSlider;
    [SerializeField]
    private GameObject _pauseScreen;

    private GameObject _titleScreen;

    private float _spawnRate = 2.0f;
    private int _score = 0;
    private int _lives = 3;

    public bool isGameActive;
    public bool isGamePaused;

    void Start()
    {
        _titleScreen = GameObject.Find("TitleScreen");
        _soundSlider.onValueChanged.AddListener(delegate { ChangeMusicVolume(); });
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isGameActive)
        {
            if (!isGamePaused)
            {
                Time.timeScale = 0;
                _backgroundMusic.Pause();
                _pauseScreen.SetActive(true);
                isGamePaused = true;
            }
            else
            {
                Time.timeScale = 1;
                _backgroundMusic.Play();
                _pauseScreen.SetActive(false);
                isGamePaused = false;
            }
        }
    }

    public void UpdateScore(int scoreAmount)
    {
        _score += scoreAmount;

        if (_score < 0) _score = 0;

        _scoreText.text = "SCORE: " + _score;
    }

    public void UpdateLives(int livesAmount)
    {
        _lives += livesAmount;

        if (_lives <= 0)
        {
            _lives = 0;
            GameOver();
        }

        _livesText.text = "LIVES: " + _lives;
    }

    public void StartGame(int difficulty)
    {
        isGameActive = true;
        _titleScreen.SetActive(false);

        _spawnRate /= difficulty;
        StartCoroutine(SpawnTarget());

        UpdateLives(0);
        UpdateScore(0);
    }

    void GameOver()
    {
        _restartButton.gameObject.SetActive(true);
        _gameOverText.gameObject.SetActive(true);
        _backgroundMusic.Stop();
        isGameActive = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ChangeMusicVolume()
    {
        _backgroundMusic.volume = _soundSlider.value;
    }

    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            int index = Random.Range(0, _targets.Count);
            Instantiate(_targets[index]);

            yield return new WaitForSeconds(_spawnRate);
        }
    }
}
