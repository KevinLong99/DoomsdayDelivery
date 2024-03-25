using UnityEngine;
using System.Collections;

public class SuppplyDelete : MonoBehaviour
{
    public void CheckAndDeleteSupplies()
    {
        StartCoroutine(DelayedDeleteSupplies());
    }

    // This coroutine handles the delay and the deletion process
    private IEnumerator DelayedDeleteSupplies()
    {
        GameObject objectDetector = GameObject.Find("Object Detector");
        if (objectDetector != null)
        {
            yield return new WaitForSeconds(0.15f); // Delay

            NewObjectCounter newObjectCounter = objectDetector.GetComponent<NewObjectCounter>();
            if (newObjectCounter != null)
            {
                newObjectCounter.DeleteAllSupply();
            }
            else
            {
                Debug.LogError("NewObjectCounter script not found on Object Detector.");
            }
        }
        else
        {
            Debug.LogError("Object Detector GameObject not found in the scene.");
        }
    }
}