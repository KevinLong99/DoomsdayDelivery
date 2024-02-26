using PA_DronePack;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableDroneControl : MonoBehaviour
{
    // Start is called before the first frame update

    public PA_DroneAxisInput DroneScript;

    void Start()
    {
        DroneScript.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
