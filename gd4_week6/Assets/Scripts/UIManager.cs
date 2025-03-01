using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI gameOverScoreText;
    public GameObject startFruit;
    public GameObject inGameUI;
    public GameObject startUI;
    public GameObject spawnManager;
    public GameObject GameOverUI;
    public int score = 0;
    int lives = 3;

    [SerializeField] GameObject lineGO;
    Line line;

    private void Start()
    {
        line = lineGO.GetComponent<Line>();
    }

    public void UpdateScore(int scoreToAdd)
    {

        score += scoreToAdd;
        scoreText.text = "SCORE : " + score.ToString();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log($"Gameover call");
            GameOver();
        }
    }

    public void UpdateLives(int livesToAdd)
    {
        lives += livesToAdd;
        livesText.text = "LIVES : " + lives.ToString();
        if (lives <=0)
        {
            GameOver();
        }
    }

    public void StartGame()
    {
        inGameUI.SetActive(true);
        startUI.SetActive(false);
        spawnManager.SetActive(true);
    }

    public void GameOver()
    {
        GameOverUI.SetActive(true);
        inGameUI.SetActive(false);
        spawnManager.SetActive(false);
        gameOverScoreText.text = "YOUR SCORE: " + score.ToString();
        line.gameOver = true;

    }

    public void SetVolume(int volume)
    {
        AudioListener.volume = volume;
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
