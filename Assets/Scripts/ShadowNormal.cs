using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowNormal : MonoBehaviour
{
    private Vector3 shadowPosition = new Vector3(0.4f,-0.3f,0);
    void Start()
    {
        GetComponent<SpriteRenderer>().color = new Color(0,0,0, 0.25f);
    }

    void Update()
    {
        transform.position = transform.parent.position + shadowPosition;
    }
}
