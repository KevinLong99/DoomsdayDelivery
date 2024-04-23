using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.XR.CoreUtils;
using UnityEditor;
using UnityEngine.SceneManagement;

public class ReturnPlayerToStartScene : MonoBehaviour
{
    public InputActionProperty toMenuButton;

    public void Recenter()
    {
        Debug.Log("test_Nick");
    }
}


#if UNITY_EDITOR
[CustomEditor(typeof(ReturnPlayerToStartScene))]
public class ToMenuScene : Editor
{
    public override void OnInspectorGUI()
    {

        base.OnInspectorGUI();
        if (GUILayout.Button("ToMenuScene"))
        {
            string menuScene = "DoomsdayDelivery_Menu";
            SceneManager.LoadScene(menuScene);
        }
    }
}
#endif