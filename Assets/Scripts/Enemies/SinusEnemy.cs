using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinusEnemy : EnemyBasic
{
    [Header ("Sinus enemy")]
    public float amplitude = 1f;
    public float frequency = 1f;

    private bool canAttack;
    private bool hasAttacked;

    protected override void Move() {
        base.Move();
        float sinValue = Mathf.Sin(Time.time * frequency);
        transform.position = transform.position
            + transform.right * sinValue * amplitude * Time.deltaTime;
        
        canAttack = sinValue > -0.01 && sinValue < 0.01;
        if (!canAttack) {
            hasAttacked = false;
        }
    }

    protected override void Attack() {
        if (canAttack && !hasAttacked) {
            InstantiateBullet();
            hasAttacked = true;
        }
    }

}
