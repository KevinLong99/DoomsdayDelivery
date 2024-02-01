using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actuate_PlayerController_DD : MonoBehaviour
{
    public GameObject objectToControl;
    public Actuate.ActuateAgent actuateAgent;

    // Use this for initialization
    void Start()
    {
        actuateAgent.SetMotionSource(this.gameObject);  //do i need the "extra data" as in the original script?
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.position = objectToControl.transform.position;
    }
}
