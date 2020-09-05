using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShot : MonoBehaviour
{
    public float speed = 5f;
    private BulletType type = BulletType.DEFAULT;

    private GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    public void SetBulletType(BulletType newType) {
        type = newType;
        GetComponent<SpriteRenderer>().color = type.GetColor();
    }

    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
        if (transform.position.y < -5.5) {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("Ship")) {
            Destroy(gameObject);
            gameManager.HitTaken();
        } else if (col.CompareTag("Barrier") && col.gameObject.GetComponent<BarrierModifier>().modifierType.Equals(type)) {
            Destroy(gameObject);
        }
    }
}