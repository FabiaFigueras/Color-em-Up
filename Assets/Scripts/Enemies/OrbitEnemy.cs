using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum ORBIT_STATE {
    INITIAL_POSITION_MOVEMENT,
    INITIAL_ROTATION,
    ORBIT,
    FINAL_ROTATION,
    ENDING_MOVEMENT
}

public enum SENSE_OF_ROTATION {
    ClockWise = -1,
    Counterclockwise = 1
}


public class OrbitEnemy : EnemyBasic
{
    [Header ("Orbit Enemy")]
    public GameObject barrier;
    public float rotationSpeed;
    public SENSE_OF_ROTATION rotationSense = SENSE_OF_ROTATION.ClockWise;
    public bool attackTowardsEnemy = false;
    public int numberOfAttacks = 10;

    private ORBIT_STATE currentState;
    public Vector3 initialRotationPosition = new Vector3(0,3,0);
    private float angleRotated;
    private float attackAngle;
    private float currentAttackAngle;

    protected override void SetSpecificInfo()
    {
        base.SetSpecificInfo();
        attackAngle = 360 / numberOfAttacks;
        currentAttackAngle = 0;
    }

    protected override void Move() {
        switch (currentState) {
            case ORBIT_STATE.INITIAL_POSITION_MOVEMENT:
                InitialMovement();
                break;
            case ORBIT_STATE.INITIAL_ROTATION:
                InitialRotation();
                break;
            case ORBIT_STATE.ORBIT:
                Orbit();
                break;
            case ORBIT_STATE.FINAL_ROTATION:
                FinalRotation();
                break;
            case ORBIT_STATE.ENDING_MOVEMENT:
                base.Move();
                break;
        }
    }

    private void InitialMovement() {
        transform.position = Vector3.MoveTowards(transform.position, initialRotationPosition, movementSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, initialRotationPosition) < 0.01) {
            currentState = ORBIT_STATE.INITIAL_ROTATION;
            angleRotated = 0;
        }
    }

    private void InitialRotation() {
        float angleStep = rotationSpeed * Time.deltaTime;
        transform.RotateAround(transform.position, -Vector3.forward * (int)rotationSense, angleStep);
        angleRotated += angleStep;
        if (angleRotated >= 90) {
            currentState = ORBIT_STATE.ORBIT;
            angleRotated = 0;
        }
    }

    private void Orbit() {
        float angleStep = rotationSpeed * Time.deltaTime;
        transform.RotateAround(Vector3.zero, transform.forward * (int)rotationSense, angleStep);
        angleRotated += angleStep;
        
        // Check if we have to attack
        currentAttackAngle += angleStep;
        if (currentAttackAngle >= attackAngle) {
            Instantiate(bullet, transform.position, Quaternion.Euler(transform.right));
            currentAttackAngle = 0;
        }


        if (angleRotated >= 360) {
            currentState = ORBIT_STATE.FINAL_ROTATION;
            angleRotated = 0;
        }
    }

    private void FinalRotation() {
        float angleStep = rotationSpeed * Time.deltaTime;
        transform.RotateAround(transform.position, -Vector3.forward * (int)rotationSense, angleStep);
        angleRotated += angleStep;
        if (angleRotated >= 90) {
            currentState = ORBIT_STATE.ENDING_MOVEMENT;
            angleRotated = 0;
        }
    }

    protected override void Attack() {}

}
