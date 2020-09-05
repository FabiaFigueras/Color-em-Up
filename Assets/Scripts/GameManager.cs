using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public int shipPower = 3;
    
    private ShakeController mainCamera;
    private ShipController player;
    private int score = 0;
    public int scoreLostPerLive = 50;

    void Start()
    {
        mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<ShakeController>();
        player = GameObject.FindWithTag("Player").GetComponent<ShipController>();
    }

    void Update()
    {
        
    }

    public void EnemyDestroyed(int scoreToAdd) {
        score += scoreToAdd;
        // Debug.Log("New score: " + score + "[Enemy destroyed]");
    }

    public void NewWave(int waveScore) {
        score += waveScore;
        // Debug.Log("New score: " + score + "[New wave]");
    }

    public void HitTaken() {
        score -= scoreLostPerLive;
        // Debug.Log("New score: " + score + " [Hit taken]");
        mainCamera.TriggerShake();
        shipPower--;
        if (shipPower <= 0) {
            Debug.Log("No power left: GAME OVER");
            player.DestroyShip();
        }
    }
}
