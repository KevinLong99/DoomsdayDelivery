using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateFog : MonoBehaviour
{
    public float scale = .01f;

    private float xDir;
    private float yDir;
    private float zDir;

    private void Start()
    {
        xDir = Random.Range(-1.0f, 1.0f);
        yDir = Random.Range(-1.0f, 1.0f);
        zDir = Random.Range(-1.0f, 1.0f);
}
    void FixedUpdate()
    {
        transform.Rotate(xDir * scale, yDir * scale, zDir * scale);
    }
}
