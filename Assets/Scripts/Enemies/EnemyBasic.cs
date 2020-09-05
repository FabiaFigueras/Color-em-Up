using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *  Basic Enemy
 *  - Moves in a straight line (not necessarily down)
 */
public class EnemyBasic : MonoBehaviour
{
    private GameObject player;

    [Header("Unit attributes")]
    public Vector3 initialPosition;
    public Vector3 initialRotation;
    public float movementSpeed;

    [Header("Combat attributes")]
    public float timeBetweenAttacks = 1f;
    private float timePassedBetweenAttacks;
    public EnemyShot bullet;
    public BulletType bulletWeakness;
    public int pointsAwarded;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        timePassedBetweenAttacks = timeBetweenAttacks;
        bullet.SetBulletType(bulletWeakness);
        GetComponent<SpriteRenderer>().color = bulletWeakness.GetColor();
    }

    void Update()
    {
        Move();
        Attack();
    }

    protected void Move() {
        transform.Translate(0, -movementSpeed * Time.deltaTime, 0, Space.World);
    }

    protected void Attack() {
        if (timePassedBetweenAttacks < timeBetweenAttacks) {
            timePassedBetweenAttacks += Time.deltaTime;
            return;
        }

        timePassedBetweenAttacks = 0;
        Instantiate(bullet, transform.position, transform.rotation);
    }

    protected Vector3 GetPointToLookAt() {
        return Vector3.down;
    }

    protected Vector3 GetAttackDirection() {
        return Vector3.down;
    }

    protected int GetPoints() {
        return pointsAwarded;
    }
}
