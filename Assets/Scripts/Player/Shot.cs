using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    public float speed = 10f;
    private BulletType type = BulletType.DEFAULT;
    private bool barrierCollided = false;

    private GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
        if (transform.position.y > (gameManager.yRange + 0.5f)) {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (!barrierCollided && col.CompareTag("Barrier")) {
            barrierCollided = true;
            // Change to the element of that barrier part
            type = col.gameObject.GetComponent<BarrierModifier>().modifierType;
            GetComponent<SpriteRenderer>().color = type.GetColor();
        } else if (col.CompareTag("Enemy")) {
            EnemyBasic enemyCol = col.gameObject.GetComponent<EnemyBasic>();
            if (enemyCol.CanBeDamaged(type)) {
                gameManager.EnemyDestroyed(enemyCol.pointsAwarded);
                enemyCol.DestroyShip();
                Destroy(gameObject);
            }
        }
    }
}

public static class Extensions {
    public static Color GetColor(this BulletType type) {
        switch (type) {
            case BulletType.RED:
                return Color.red;
            case BulletType.GREEN:
                return Color.green;
            case BulletType.BLUE:
                return Color.blue;
            default:
                return Color.white;
        }
    }

    public static bool Equals(this BulletType type, BulletType other) {
        return type == other;
    }
}

public enum BulletType {
    DEFAULT,
    RED,
    GREEN,
    BLUE
}
