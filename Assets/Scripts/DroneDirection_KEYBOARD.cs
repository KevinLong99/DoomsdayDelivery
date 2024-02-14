using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DroneDirection_KEYBOARD : MonoBehaviour
{
    Rigidbody rb;

    private Vector3 force1x = new Vector3(1, 0, 0);
    private Vector3 force2x = new Vector3(-1, 0, 0);
    private Vector3 force3x = new Vector3(0, 0, 1);
    private Vector3 force4x = new Vector3(0, 0, -1);

    private Vector3 moveZaxis = new Vector3(0, 0, 5);
    private Vector3 moveXaxis = new Vector3(5, 0, 0);

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            //rb.AddForce(force3x, ForceMode.Impulse);
            this.transform.position += moveZaxis;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            //rb.AddForce(force4x, ForceMode.Impulse);
            this.transform.position -= moveZaxis;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //rb.AddForce(force2x, ForceMode.Impulse);
            this.transform.position -= moveXaxis;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //rb.AddForce(force1x, ForceMode.Impulse);
            this.transform.position += moveXaxis;
        }
    }
}
