using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaController : MonoBehaviour
{
    public float speed = 1f;
    public float resetPoint = -10f;
    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        transform.Translate(0, -speed * Time.deltaTime, 0);
        if (transform.position.y <= -10) {
            transform.position = initialPosition;
        }
    }
}
