using UnityEngine;
using System.Collections; // Needed for IEnumerator

public class SwitchReturn : MonoBehaviour
{
    // Assign this in the inspector to the transform you want to move this object to
    public Transform targetTransform;

    private Vector3 lastPosition;
    private bool isCheckingMovement = false;

    // This function starts the movement check coroutine
    public void TrySetPositionToTarget()
    {
        Debug.Log("function called man.");
        if (!isCheckingMovement)
        {
            StartCoroutine(CheckMovementAndSetPosition());
        }
    }

    // Coroutine to check if the GameObject is moving for 1 second
    private IEnumerator CheckMovementAndSetPosition()
    {
        isCheckingMovement = true;
        //lastPosition = transform.position; // Store the current position
        yield return new WaitForSeconds(0.5f); // Wait for half a second

        transform.position = targetTransform.position;

        // Check if the GameObject has moved in the last second
        if (transform.position == lastPosition)
        {
            // If not, and the targetTransform is assigned, set the GameObject's position
            if (targetTransform != null)
            {
                transform.position = targetTransform.position;
                Debug.Log("Transform is changed bro");
            }
            else
            {
                //Debug.LogError("Target Transform is not assigned.");
                Debug.Log("position no change bro.");
            }
        }
        // If the GameObject has moved, do nothing
        isCheckingMovement = false;
    }
}