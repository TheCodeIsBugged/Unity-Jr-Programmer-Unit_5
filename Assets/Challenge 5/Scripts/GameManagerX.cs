﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerX : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public GameObject titleScreen;
    public Button restartButton; 

    public List<GameObject> targetPrefabs;

    private int score;
    private float spawnRate = 1.5f;
    public bool isGameActive;

    private float spaceBetweenSquares = 2.5f; 
    private float minValueX = -3.75f; //  x value of the center of the left-most square
    private float minValueY = -3.75f; //  y value of the center of the bottom-most square

    public TextMeshProUGUI timeText;
    private static float gameTime = 60f;

    public TextMeshProUGUI lifeText;
    public int life = 3;

    public AudioSource volumeSource;
    public Slider volumeSlider;

    private bool isPaused = false;
    public GameObject pausePanel;
    // Start the game, remove title screen, reset score, and adjust spawnRate based on difficulty button clicked
    public void StartGame(float spawnRateDifficulty)
    {
        spawnRate /= spawnRateDifficulty;
        isGameActive = true;
        StartCoroutine(SpawnTarget());
        score = 0;
        UpdateScore(score);
        titleScreen.SetActive(false);
        UpdateLife();

        
    }

    // While game is active spawn a random target
    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targetPrefabs.Count);

            if (isGameActive)
            {
                Instantiate(targetPrefabs[index], RandomSpawnPosition(), targetPrefabs[index].transform.rotation);
            }
            
        }
    }

    // Generate a random spawn position based on a random index from 0 to 3
    Vector3 RandomSpawnPosition()
    {
        float spawnPosX = minValueX + (RandomSquareIndex() * spaceBetweenSquares);
        float spawnPosY = minValueY + (RandomSquareIndex() * spaceBetweenSquares);

        Vector3 spawnPosition = new Vector3(spawnPosX, spawnPosY, 0);
        return spawnPosition;

    }

    // Generates random square index from 0 to 3, which determines which square the target will appear in
    int RandomSquareIndex()
    {
        return Random.Range(0, 4);
    }

    // Update score with value from target clicked
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = $"Score: {score.ToString()}";
    }

    // Stop game, bring up game over text and restart button
    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        isGameActive = false;
    }

    // Restart game by reloading the scene
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void UpdateTime()
    {
        gameTime -= Time.deltaTime;
        timeText.text = $"Time: {gameTime.ToString("n2")}";
        if (gameTime <= 0)
        {
            gameTime = 0;
            GameOver();
        }
    }

    void Update()
    {
        if (isGameActive)
        {
            UpdateTime();
        }

        if (life <= 0)
        {
            GameOver();
        }

        volumeSource.volume = volumeSlider.value;

        if (Input.GetKeyDown(KeyCode.Space) && !isPaused)
        {
            isPaused = true;
            Time.timeScale = 0;
            pausePanel.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Space) && isPaused)
        {
            isPaused = false;
            Time.timeScale = 1;
            pausePanel.SetActive(false);
        }
    }

    public void UpdateLife()
    {
        lifeText.text = $"Life: {life}";
    }
}
