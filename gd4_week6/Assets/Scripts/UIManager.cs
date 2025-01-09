using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public GameObject startFruit;
    public GameObject inGameUI;
    public GameObject startUI;
    public GameObject spawnManager;
    public int score = 0;
    int lives = 3;
    
    public void UpdateScore(int scoreToAdd)
    {

        score += scoreToAdd;
        scoreText.text = "SCORE : " + score.ToString();

    }

    public void UpdateLives(int livesToAdd)
    {
        lives += livesToAdd;
        livesText.text = "LIVES : " + lives.ToString();
    }

    public void StartGame()
    {
        Target startFruitTarget = startFruit.GetComponent<Target>();
        startFruitTarget.isHit = true;

        inGameUI.SetActive(true);
        startUI.SetActive(false);
        spawnManager.SetActive(true);
    }
}
