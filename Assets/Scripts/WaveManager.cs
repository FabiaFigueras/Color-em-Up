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

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        // Remove one of the orbit enemies waves
        waves.RemoveAt(3);
    }

    private void InitSpawning() {
        Debug.Log("Init spawning");
        if (waves.Count > 0) {
            
            currentWave = Random.Range(0, waves.Count);
            // currentWave = 9;

            spawnCoroutine = SendNextWave(waves[currentWave].waveTime);
            StartCoroutine(spawnCoroutine);
        }
    }

    IEnumerator SendNextWave(float timeToWait) {
        waveTimeModifier -= 0.025f;
        InstantiateEnemies();
        
        currentWave = Random.Range(0, waves.Count);
        // currentWave = 9;

        Debug.Log("New wave: "+ currentWave);
        gameManager.NewWave();

        yield return new WaitForSeconds(Mathf.Max(2f, timeToWait * waveTimeModifier));
        spawnCoroutine = SendNextWave(waves[currentWave].waveTime);
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
            enemy.timeBetweenAttacks = wave.enemies[i].timeBetweenAttacks;
            enemy.movementSpeed = wave.enemies[i].speed;
            enemy.rotationSense = wave.enemies[i].rotationDirection;
        }
    }

    private void StopSpawning() {
        if (spawnCoroutine != null) {
            StopCoroutine(spawnCoroutine);
        }
    }

    public void Restart() {
        audioSource.Play();
        StopSpawning();
        waveTimeModifier = 1f;
        currentWave = 0;
        InitSpawning();
    }

}

[System.Serializable]
public class Wave {
    public float waveTime = 5f;
    public List<EnemyInWave> enemies;
}

[System.Serializable]
public class EnemyInWave {
    public EnemyBasic enemy;
    public Vector3 position = new Vector3(0,0,0);
    public int inclination = 0;
    public BulletType type = BulletType.DEFAULT; // Type equals weakness
    public float timeBetweenAttacks = 2.5f;
    public float speed = 1f;
    public SENSE_OF_ROTATION rotationDirection = SENSE_OF_ROTATION.ClockWise;
}
