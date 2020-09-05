using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public int playerLives = 3;
    
    private int score = 0;
    public int scoreLostPerHit = 5;

    void Start()
    {
        
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
        score -= scoreLostPerHit;
        Debug.Log("New score: " + score + " [Hit taken]");
    }
}
