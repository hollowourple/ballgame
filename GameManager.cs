using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOver;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI pauseText;
    public Button restartButton;
    public GameObject titleScreen;
    private int score;
    private float spawnRate = 2.0f;
    public bool isGameActive;
    public int lives = 3;
    public bool pause;

    void Update()
    {
        pauseGame();
    }
   
    IEnumerator SpawnTarget()
    {
        while(isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            if (isGameActive)
            {
                int index = Random.Range(0, targets.Count);
                Instantiate(targets[index]);
            }

        }
    }
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }
    public void GameOver()
    {
        restartButton.gameObject.SetActive(true);
        gameOver.gameObject.SetActive(true);
        isGameActive = false;
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void pauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pause = true;
            if (pause == true)
            {
                Time.timeScale = 0f;
                pauseText.gameObject.SetActive(true);
            }
            else
            {
                pauseText.gameObject.SetActive(false);
                Time.timeScale = 1f;
            }
        }
    }
    public void UpdateLives(int livesUpdate)
    {
        lives += livesUpdate ;
        if (lives <= 0)
        {
            lives = 0;
            GameOver();
        }
        livesText.text = "Lives: " + lives;
    }
    public void StartGame(int difficulty)
    {
        isGameActive = true;
        score = 0;
        lives = 3; //resetting lives when restarting
        livesText.text = "Lives: " + lives;
        spawnRate /= difficulty;
        StartCoroutine(SpawnTarget());
        UpdateScore(0);
        titleScreen.gameObject.SetActive(false);
    }
    
}
