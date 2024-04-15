using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.XR.CoreUtils;

public class RecenterOrigin : MonoBehaviour
{
    public Transform head;
    public Transform origin;
    public Transform target;

    public InputActionProperty recenterButton;

    public void Recenter()
    {
        XROrigin xrOrigin = GetComponent<XROrigin>();
        xrOrigin.MoveCameraToWorldLocation(target.position);
        xrOrigin.MatchOriginUpCameraForward(target.up, target.forward);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Recenter();
            Debug.Log("HIT");
        }
        if(recenterButton.action.WasPressedThisFrame())
        {
            Recenter();
            Debug.Log("HIT2");
        }
    }
}
