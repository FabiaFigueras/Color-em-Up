using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public List<Wave> waves;
    private int currentWave = 0;

    void Start()
    {
        if (waves.Count > 0) {
            StartCoroutine(SendNextWave(waves[0].timeAfterLastWave));
        }
    }

    IEnumerator SendNextWave(float timeToWait) {
        yield return new WaitForSeconds(timeToWait);
        InstantiateEnemies();
        currentWave++;
        if (currentWave < waves.Count) {
            StartCoroutine(SendNextWave(waves[currentWave].timeAfterLastWave));
        } else {
            Debug.Log("No more waves");
        }
    }

    private void InstantiateEnemies() {
        Wave wave = waves[currentWave];
        for (int i = 0; i < wave.enemies.Count; i++) {
            Instantiate(
                wave.enemies[i].enemy,
                transform.position + wave.enemies[i].position, 
                Quaternion.Euler(wave.enemies[i].rotation)
            );
            wave.enemies[i].enemy.bulletWeakness = wave.enemies[i].type;
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
    public Vector3 position;
    public Vector3 rotation;
    public BulletType type; // Type equals weakness
}
