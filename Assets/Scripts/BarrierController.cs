using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierController : MonoBehaviour
{

    public float rotationSpeed = 100f;

    void Start()
    {
        
    }

    void Update()
    {
    }

    void FixedUpdate() {
        int rotationDirection = 0;
        if (Input.GetKey(KeyCode.RightArrow)) {
            rotationDirection = -1;
        } else if (Input.GetKey(KeyCode.LeftArrow)) {
            rotationDirection = 1;
        }

        transform.Rotate (Vector3.forward * rotationDirection * rotationSpeed * Time.deltaTime);
    }
}
