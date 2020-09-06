using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{

    public int shipPower = 3;
    
    public TextMeshProUGUI storedHighScore;
    public TextMeshProUGUI currentScore;
    public TextMeshProUGUI livesText;

    public GameObject gameOverScreen;
    private GameObject initialScreen;
    public GameObject restartPLayer;

    private ShakeController mainCamera;
    private ShipController player;
    private int score = 0;
    private int highScore;
    public int scoreLostPerLive = 50;

    public float xRange = 3.5f;
    public float yRange = 4.75f;

    void Start()
    {
        initialScreen = GameObject.Find("Initial_Screen");

        highScore = PlayerPrefs.GetInt("highscore", 0);
        storedHighScore.text = "HIGH SCORE: " + highScore.ToString("D6");

        mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<ShakeController>();
        player = GameObject.FindWithTag("Player").GetComponent<ShipController>();
        UpdateLives();
    }

    private void UpdateLives(int value = 0) {
        if (value == 0) {
            shipPower = 3;  // Reset to original value
        } else {
            shipPower += value;
        }
        livesText.text = "x" + shipPower;
    }

    public void EnemyDestroyed(int scoreToAdd) {
        if (shipPower > 0) {
            UpdateScore(scoreToAdd);
        }
    }

    public void NewWave(int waveScore) {
        if (shipPower > 0) {
            UpdateScore(waveScore);
        }
    }

    public void HitTaken() {
        if (shipPower > 0) {
            mainCamera.TriggerShake();
            UpdateLives(-1);
            if (shipPower <= 0) {
                Debug.Log("No power left: GAME OVER");
                player.DestroyShip();
            } else {
                UpdateScore(-scoreLostPerLive);
            }
        }
    }

    private void UpdateScore(int scoreToAdd) {
        score = Mathf.Max(0, score + scoreToAdd);
        currentScore.text = "SCORE: " + score.ToString("D6");
        if (score > highScore) {
            highScore = score;
            storedHighScore.text = "HIGH SCORE: " + highScore.ToString("D6");
            PlayerPrefs.SetInt("highscore", highScore);
            PlayerPrefs.Save();
        } 
    }

    public bool InsidePlayBounds(Vector3 position) {
        bool validX = position.x < (xRange * 1.1f) && position.x > (-xRange * 1.1f);
        bool validY = position.y < yRange && position.y > -yRange;
        return validX && validY;
    }

    public void RestartGame() {
        // Detect all enemies
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject enemy in enemies) {
            Destroy(enemy);
        }
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
        foreach(GameObject bullet in bullets) {
            Destroy(bullet);
        }
        Destroy(player);
        Instantiate(restartPLayer, new Vector3(0,-4,0), restartPLayer.transform.rotation);
        player = GameObject.FindWithTag("Player").GetComponent<ShipController>();
        ShowGameOverScreen(false);
        shipPower = 3;
        UpdateLives();

        if (initialScreen != null) {
            Destroy(initialScreen);
        }
    }

    public void ShowGameOverScreen(bool toShow) {
        gameOverScreen.SetActive(toShow);
    }

}
