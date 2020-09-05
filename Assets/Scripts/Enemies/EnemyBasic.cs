﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *  Basic Enemy
 *  - Moves in a straight line (not necessarily down)
 */
public class EnemyBasic : MonoBehaviour
{
    private GameManager gameManager;
    private GameObject player;
    private ParticleSystem deathParticleSystem;
    private Collider2D shipCollider;
    private Renderer shipRenderer;

    [Header("Unit attributes")]
    public Vector3 initialPosition;
    public Vector3 initialRotation;
    public float movementSpeed;
    private bool isAlive = true;

    [Header("Combat attributes")]
    public float timeBetweenAttacks = 1f;
    private float timePassedBetweenAttacks;
    public EnemyShot bullet;
    public BulletType bulletWeakness;
    public int pointsAwarded;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        deathParticleSystem = GetComponentInChildren<ParticleSystem>();
        shipCollider = GetComponent<Collider2D>();
        shipRenderer = GetComponent<Renderer>();

        timePassedBetweenAttacks = timeBetweenAttacks;
        GetComponent<SpriteRenderer>().color = bulletWeakness.GetColor();
    }

    void Update()
    {
        if (isAlive) {
            Move();
            Attack();

            CheckDeathBounds();
        }
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
        EnemyShot newBullet = Instantiate(bullet, transform.position, transform.rotation);
        newBullet.SetBulletType(bulletWeakness);
    }

    private void CheckDeathBounds() {
        if (transform.position.y <= -6) {
            Destroy(gameObject);
        }
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

    public bool CanBeDamaged(BulletType shotType) {
        if (bulletWeakness == BulletType.DEFAULT) {
            return true;
        } else {
            return bulletWeakness.Equals(shotType);
        }
    }

    public void DestroyShip() {
        isAlive = false;
        Destroy(shipCollider);
        deathParticleSystem.Play();
        StartCoroutine(RemoveShip());
    }

    IEnumerator RemoveShip() {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
    
    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.CompareTag("Ship")) {
            gameManager.HitTaken();
            DestroyShip();
        }
    }
}
