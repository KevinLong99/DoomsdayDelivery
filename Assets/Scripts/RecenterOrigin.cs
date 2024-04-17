using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.XR.CoreUtils;
using UnityEditor;

public class RecenterOrigin : MonoBehaviour
{
    [Header("Adjust Player X Pos")]
    [SerializeField] [Range(0, 50)]
    float playerXPos;

    [Header("Adjust Player Z Pos")]
    [SerializeField] [Range(0, 50)]
    float playerZPos;

    public Transform head;
    public Transform origin;
    public Transform target;

    public InputActionProperty recenterButton;

    public void Recenter()
    {
        XROrigin xrOrigin = GetComponent<XROrigin>();
        xrOrigin.MoveCameraToWorldLocation(target.position);
        xrOrigin.MatchOriginUpCameraForward(target.up, target.forward);
        Debug.Log("test");
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(RecenterOrigin))]
public class UnityGUIButtonEditor : Editor
{
    public override void OnInspectorGUI()
    {
        RecenterOrigin recenter = (RecenterOrigin)target;
            
        base.OnInspectorGUI();
        if(GUILayout.Button("Recenter Player"))
        {
            recenter.Recenter();
        }
    }
}
#endif