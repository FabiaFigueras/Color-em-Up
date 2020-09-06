using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public GameObject ship;
    public ParticleSystem deathParticles;
    private Collider2D shipCollider;
    private GameManager gameManager;

    public bool isAlive = true;
    public float movementSpeed = 5f;
    public GameObject bullet;
    public float secondsBetweenShots = 0.2f;
    private float lastShotTime;

    void Start()
    {
        lastShotTime = secondsBetweenShots;
        shipCollider = ship.GetComponent<Collider2D>();
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
    }

    void Update()
    {
        if (isAlive) {
            Move();
            Shoot();
        }
    }

    void Move() {
        int verticalMovement = 0;
        int horizontalMovement = 0;
        
        // Vertical input
        if (Input.GetKey(KeyCode.W)) {
            verticalMovement = 1;
        } else if (Input.GetKey(KeyCode.S)) {
            verticalMovement = -1;
        }

        // Horizontal input
        if (Input.GetKey(KeyCode.D)) {
            horizontalMovement = 1;
        } else if (Input.GetKey(KeyCode.A)) {
            horizontalMovement = -1;
        }

        transform.Translate(new Vector3(
            horizontalMovement * movementSpeed * Time.deltaTime,
            verticalMovement * movementSpeed * Time.deltaTime,
            0)
        );

        transform.position = new Vector3(
            Mathf.Max(-gameManager.xRange, Mathf.Min(transform.position.x, gameManager.xRange)),
            Mathf.Max(-gameManager.yRange, Mathf.Min(transform.position.y, gameManager.yRange)),
            transform.position.z
        );
    }

    void Shoot() {
        lastShotTime += Time.deltaTime;
        if (Input.GetKey(KeyCode.UpArrow) && lastShotTime >= secondsBetweenShots) {
            lastShotTime = 0f;
            Instantiate(bullet, transform.position, transform.rotation);
        }
    }

    public void DestroyShip() {
        isAlive = false;
        Instantiate(deathParticles, transform.position, transform.rotation);
        StartCoroutine(RemoveShip());
    }

    IEnumerator RemoveShip() {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
        gameManager.ShowGameOverScreen(true);
    }
}
