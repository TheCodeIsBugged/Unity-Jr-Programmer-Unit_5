using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> target;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameoverText;
    public Button restartButton;
    public GameObject scoreUI;

    private float spawnRate = 3f;
    private int score;
    public bool isGameover;

    public TextMeshProUGUI lifeText;
    private int life;

    public AudioSource volumeSource;
    public Slider volumeSlider;

    public GameObject pausePanel;
    public bool isPaused = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //ChangeVolume();
        PauseGame();
    }

    IEnumerator SpawnTarget(float spawnRateDifficulty)
    {
        while (!isGameover)
        {
            yield return new WaitForSeconds(spawnRate);
            int randomIndex = Random.Range(0, target.Count);
            Instantiate(target[randomIndex]);
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        if (!isGameover)
        {
            scoreText.text = score.ToString();
        }
    }

    public void Gameover()
    {
        gameoverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        isGameover = true;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void StartGame(float difficultyLevel)
    {
        scoreUI.SetActive(true);

        spawnRate /= difficultyLevel;
        isGameover = false;
        score = 0;
        StartCoroutine(SpawnTarget(spawnRate));
        UpdateScore(score);
        UpdateLife(3);
    }

    public void UpdateLife(int lifeToAdd)
    {
        life += lifeToAdd;
        lifeText.text = $"Life: {life}";
        lifeText.gameObject.SetActive(true);
        if (life <= 0)
        {
            life = 0;
            Gameover();
        }
    }

    public void ChangeVolume()
    {
        volumeSource.volume = volumeSlider.value;
    }

    public void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isPaused)
        {
            Time.timeScale = 1;
            isPaused = false;
            pausePanel.SetActive(false);
        }

        else if (Input.GetKeyDown(KeyCode.Space) && !isPaused)
        {
            Time.timeScale = 0;
            isPaused = true;
            pausePanel.SetActive(true);
        }
    }
}
