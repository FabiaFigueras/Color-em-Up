using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public GameManager gameManager;

    [Header("Waves")]
    public List<Wave> waves;
    private int scorePerWave = 25; // Multiplied for every wave index
 
    private int currentWave;
    private bool isActive = false;
    private IEnumerator spawnCoroutine;

    private float waveTimeModifier = 1f;

    void Start()
    {
        InitSpawning();
    }

    private void InitSpawning() {
        Debug.Log("Init spawning");
        currentWave = 0;
        if (waves.Count > 0) {
            spawnCoroutine = SendNextWave(waves[0].timeAfterLastWave);
            StartCoroutine(spawnCoroutine);
        }
    }

    IEnumerator SendNextWave(float timeToWait) {
        waveTimeModifier -= 0.1f;
        yield return new WaitForSeconds(Mathf.Max(2f, timeToWait * waveTimeModifier));
        InstantiateEnemies();
        
        currentWave = Random.Range(0, waves.Count-1);
        gameManager.NewWave(scorePerWave * currentWave);

        spawnCoroutine = SendNextWave(waves[currentWave].timeAfterLastWave);
        StartCoroutine(spawnCoroutine);
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

    private void StopSpawning() {
        StopCoroutine(spawnCoroutine);
    }

    public void Restart() {
        StopSpawning();
        waveTimeModifier = 1f;
        currentWave = 0;
        InitSpawning();
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
