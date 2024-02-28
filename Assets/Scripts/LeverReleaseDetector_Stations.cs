using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class LeverReleaseDetector_Stations : MonoBehaviour
{
    private XRBaseInteractable interactable;
    private LeverReturn_Stations leverRetStation_Script;

    void Awake()
    {
        interactable = GetComponent<XRBaseInteractable>();
        leverRetStation_Script = GetComponent<LeverReturn_Stations>();
    }

    void OnEnable()
    {
        interactable.selectExited.AddListener(HandleSelectExited);
    }

    void OnDisable()
    {
        interactable.selectExited.RemoveListener(HandleSelectExited);
    }

    private void HandleSelectExited(SelectExitEventArgs arg)
    {
        leverRetStation_Script.ReleaseLever();
    }
}
