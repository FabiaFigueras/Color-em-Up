using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasic : MonoBehaviour
{

    public float movementSpeed;

    void Start()
    {
        
    }

    void Update()
    {
        Attack();
    }

    protected virtual void Attack() {

    }
}
