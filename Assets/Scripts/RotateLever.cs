using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateLever : MonoBehaviour
{
    private Vector3 originalPosition;

    private HingeJoint hinge;
    private Rigidbody rbHingeConnectedBody;
    private bool rotatingHinge = false;
    private Rigidbody leverRb;
    private Vector3 anchorYValue;
    public Vector3 conAnchor;
    private float anchorMinLim;
    private float anchorMaxLim;
    private float anchorBounceVel;
    private JointLimits oldJointLimits;
    private Vector3 axisOrientation;
    private float springDamper;

    private float secondJointsMin, secondJointsMax;

    private Vector3 worldRotationOrientation = new Vector3(28.271f, 178.674f, -0.746f);

    void Start()
    {
        originalPosition = transform.position;

        hinge = GetComponent<HingeJoint>();
        rbHingeConnectedBody = hinge.connectedBody;
        RemoveHingeConnectedBody();
        leverRb = GetComponent<Rigidbody>();

        anchorYValue = hinge.anchor;
        conAnchor = hinge.connectedAnchor;
        anchorMinLim = hinge.limits.min;
        anchorMaxLim = hinge.limits.max;
        anchorBounceVel = hinge.limits.bounceMinVelocity;
        axisOrientation = hinge.axis;
        springDamper = hinge.spring.damper;

        oldJointLimits = hinge.limits;

        JointLimits getJointLims = hinge.limits;
        secondJointsMin = getJointLims.min;
        secondJointsMax = getJointLims.max;
    }

    public void DestroyHingeAndRigidbody()
    {
        Destroy(hinge);
        leverRb.isKinematic = true;
    }

    public void AddHingeAndRigidbody()
    {
        //this.gameObject.transform.localPosition = originalPosition;
        this.gameObject.transform.localRotation = Quaternion.identity;

        hinge = this.gameObject.AddComponent<HingeJoint>();
        JointLimits secondJointLims = hinge.limits;
        JointSpring twoJointSpring = hinge.spring;

        this.gameObject.GetComponent<HingeJoint>().anchor = anchorYValue;
        this.gameObject.GetComponent<HingeJoint>().axis = axisOrientation;
        this.gameObject.GetComponent<HingeJoint>().connectedAnchor = conAnchor;
        this.gameObject.GetComponent<HingeJoint>().useLimits = true;
        this.gameObject.GetComponent<HingeJoint>().useSpring = true;

        secondJointLims.min = secondJointsMin;
        secondJointLims.max = secondJointsMax;

        secondJointLims.bounceMinVelocity = 0.2f;

        twoJointSpring.damper = springDamper;

        hinge.limits = secondJointLims;
        hinge.spring = twoJointSpring;

        leverRb.isKinematic = false;

        this.gameObject.GetComponent<HingeJointListener>().RetrieveHingeJoint();

        

    }

    public void RemoveHingeConnectedBody()
    {
        hinge.connectedBody = null;
        
    }

    public void AttachHingeConnectedBody()
    {
        hinge.connectedBody = rbHingeConnectedBody;
        this.gameObject.transform.localRotation = Quaternion.identity;

    }

    public void RotateLeverCall(GameObject gameObjectToMove, Quaternion newRot, float duration)
    {
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
    }
}
