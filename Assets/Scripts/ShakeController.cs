using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeController: MonoBehaviour {
    private float shakeDuration = 0f;
    public float shakeMagnitude = 0.1f;
    private float dampingSpeed = 1.0f;
    Vector3 initialPosition;

    void Start() {
        initialPosition = transform.position;
    }

    void Update() {
        if (shakeDuration > 0) {
            transform.position = initialPosition + Random.insideUnitSphere * shakeMagnitude;
            shakeDuration -= Time.deltaTime * dampingSpeed;
        } else {
            shakeDuration = 0f;
            transform.position = initialPosition;
        }
    }

    public void TriggerShake() {
        shakeDuration = 0.15f;
    }
}