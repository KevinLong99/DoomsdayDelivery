using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateLever : MonoBehaviour
{
    private HingeJoint hinge;
    private Rigidbody rbHingeConnectedBody;
    private bool rotatingHinge = false;

    void Start()
    {
        hinge = GetComponent<HingeJoint>();
        rbHingeConnectedBody = hinge.connectedBody;
        RemoveHingeConnectedBody();
    }

    public void RemoveHingeConnectedBody()
    {
        hinge.connectedBody = null;
    }

    public void AttachHingeConnectedBody()
    {

        hinge.connectedBody = rbHingeConnectedBody;
    }

    public void RotateLeverCall(GameObject gameObjectToMove, Quaternion newRot, float duration)
    {
        AttachHingeConnectedBody();
        StartCoroutine(RotateLeverCoroutine(gameObjectToMove, newRot, duration));
    }

    IEnumerator RotateLeverCoroutine(GameObject gameObjectToMove, Quaternion newRot, float duration)
    {
        if (rotatingHinge)
        {
            yield break;
        }
        rotatingHinge = true;

        Quaternion currentRot = this.transform.rotation;

        float counter = 0;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            this.transform.rotation = Quaternion.Lerp(currentRot, newRot, counter / duration);
            yield return null;
        }
        rotatingHinge = false;
        RemoveHingeConnectedBody();
    }
}
