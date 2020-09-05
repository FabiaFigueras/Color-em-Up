using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public GameManager gameManager;

    [Header("Waves")]
    [Range(1f, 15f)]
    public int startingWave = 1;
    public List<Wave> waves;
    private int scorePerWave = 25; // Multiplied for every wave index
 
    private int currentWave = 0;

    void Start()
    {
        startingWave--; // Adapt it to list indexes
        currentWave = startingWave;
        if (waves.Count > 0 && startingWave < waves.Count) {
            StartCoroutine(SendNextWave(waves[0].timeAfterLastWave));
        }
    }

    IEnumerator SendNextWave(float timeToWait) {
        yield return new WaitForSeconds(timeToWait);
        InstantiateEnemies();
        currentWave++;
        gameManager.NewWave(scorePerWave * currentWave);

        if (currentWave < waves.Count) {
            StartCoroutine(SendNextWave(waves[currentWave].timeAfterLastWave));
        } else {
            Debug.Log("No more waves");
        }
    }

    private void InstantiateEnemies() {
        Wave wave = waves[currentWave];
        for (int i = 0; i < wave.enemies.Count; i++) {
            EnemyBasic enemy = Instantiate(
                wave.enemies[i].enemy,
                transform.position + wave.enemies[i].position, 
                Quaternion.Euler(new Vector3(0, 0, 180 + wave.enemies[i].inclination))
            );
            enemy.bulletWeakness = wave.enemies[i].type;
        }
    }

}

[System.Serializable]
public class Wave {
    public float timeAfterLastWave = 5f;
    public List<EnemyInWave> enemies;
}

[System.Serializable]
public class EnemyInWave {
    public EnemyBasic enemy;
    public Vector3 position = new Vector3(0,0,0);
    public int inclination = 0;
    public BulletType type = BulletType.DEFAULT; // Type equals weakness
}
