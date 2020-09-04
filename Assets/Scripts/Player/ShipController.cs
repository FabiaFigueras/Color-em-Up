using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{

    public float movementSpeed = 5f;
    public GameObject bullet;
    public float secondsBetweenShots = 0.2f;
    private float lastShotTime;

    private float xRange = 2.5f;
    private float yRange = 4.75f;

    void Start()
    {
        lastShotTime = secondsBetweenShots;
    }

    void Update()
    {
        Move();
        Shoot();
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
            Mathf.Max(-xRange, Mathf.Min(transform.position.x, xRange)),
            Mathf.Max(-yRange, Mathf.Min(transform.position.y, yRange)),
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
}
