using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class rotateClouds : MonoBehaviour
{
    public float scale = 0.1f;
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        transform.Rotate(0, scale, 0);
    }
}
