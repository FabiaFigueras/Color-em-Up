using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShot : MonoBehaviour
{
    public float speed = 5f;
    private BulletType type = BulletType.DEFAULT;

    void Start()
    {
        
    }

    public void SetBulletType(BulletType newType) {
        type = newType;
    }

    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
        if (transform.position.y < -5.5) {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("Player")) {
            Debug.Log("Player hit trigger");
            Destroy(gameObject);
        }
    }
}