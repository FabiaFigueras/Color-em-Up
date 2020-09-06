using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShotOrbit : EnemyShot
{

    [Header ("Orbit bullet")]
    public float secondsBeforeActivating = 2.5f;

    private bool isActive = false;
    private Vector3 movDirection;

    public void SetBulletType(BulletType newType) {
        // Does nothing in this case
    }

    public override void Prepare()
    {
        base.Prepare();
        StartCoroutine(StartMoving());
        movDirection = (Vector3.zero - transform.position).normalized;
    }

    void Update()
    {
        if (!isActive) {
            return;
        }
        transform.Translate(movDirection * speed * Time.deltaTime);
        if (!gameManager.InsidePlayBounds(transform.position)) {
            Destroy(gameObject);
        }
    }

    IEnumerator StartMoving() {
        yield return new WaitForSeconds(secondsBeforeActivating);
        isActive = true;
    }
}