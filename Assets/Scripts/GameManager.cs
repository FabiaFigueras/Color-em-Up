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

    public float xRange = 3.5f;
    public float yRange = 4.75f;

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

    public bool InsidePlayBounds(Vector3 position) {
        bool validX = position.x < (xRange * 1.1f) && position.x > (-xRange * 1.1f);
        bool validY = position.y < yRange && position.y > -yRange;
        return validX && validY;
    }
}
