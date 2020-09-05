using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public int playerLives = 3;
    
    private GameObject player;
    private int score = 0;
    public int scoreLostPerLive = 50;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        
    }

    public void EnemyDestroyed(int scoreToAdd) {
        score += scoreToAdd;
        Debug.Log("New score: " + score + "[Enemy destroyed]");
    }

    public void NewWave(int waveScore) {
        score += waveScore;
        Debug.Log("New score: " + score + "[New wave]");
    }

    public void HitTaken() {
        score -= scoreLostPerLive;
        Debug.Log("New score: " + score + " [Hit taken]");
        playerLives--;
        if (playerLives <= 0) {
            Debug.Log("No lives left: GAME OVER");
            Destroy(player);
        }
    }
}
