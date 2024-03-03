using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class BuildingRenderClip : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Building")
        {
            Transform[] allChildren = other.GetComponentsInChildren<Transform>(true);
            foreach (Transform child in allChildren)
            {
                if (child.tag != "Building")
                    child.gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Building")
        {
            Transform[] allChildren = other.GetComponentsInChildren<Transform>(true);
            foreach (Transform child in allChildren)
            {
                if (child.tag != "Building")
                    child.gameObject.SetActive(true);
            }
        }
    }
}
