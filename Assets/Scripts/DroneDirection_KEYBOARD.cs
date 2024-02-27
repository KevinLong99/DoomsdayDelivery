using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class DroneDirection_KEYBOARD : MonoBehaviour
{
    Rigidbody rb;

    private Vector3 kickForce = new Vector3(0.5f, 0, 0);

    private Vector3 force1x = new Vector3(1, 0, 0);
    private Vector3 force2x = new Vector3(-1, 0, 0);
    private Vector3 force3x = new Vector3(0, 0, 1);
    private Vector3 force4x = new Vector3(0, 0, -1);

    private Vector3 moveZaxis = new Vector3(0, 0, 5);
    private Vector3 moveXaxis = new Vector3(5, 0, 0);

    private bool isFalling = false;

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isFalling = true;
            rb.useGravity = true;
            StartCoroutine(randomFlightEvent());
        }

        if (isFalling == true)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                this.transform.position += moveZaxis;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                this.transform.position -= moveZaxis;
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                this.transform.position -= moveXaxis;
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                this.transform.position += moveXaxis;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        isFalling = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Medtent")
        {
            Debug.Log("DRONE HAS ENTERED THE MED TENT RANGE");
        }
    }

    IEnumerator randomFlightEvent()
    {
        yield return new WaitForSeconds(5);
        if (isFalling == true) 
        {
            this.transform.position += moveXaxis * 2;
            StartCoroutine(randomFlightEvent());
        } else
        {
            yield return null;
        }
    }
}
