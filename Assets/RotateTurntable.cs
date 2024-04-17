using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RotateTurntable : MonoBehaviour
{
    public float speed = 0.01f;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(0f, speed, 0f);
    }
}
