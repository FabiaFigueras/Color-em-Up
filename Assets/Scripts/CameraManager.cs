using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    void Start()
    {
        Debug.Log("Setting the resolution");
        Screen.SetResolution(1080, 1920, false);
    }

    void Update()
    {
        
    }
}
