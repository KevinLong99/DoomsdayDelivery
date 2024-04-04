using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ConstrainedGrabInteractable : XRGrabInteractable
{
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        // Apply constraints when the object is grabbed
        var rigidbody = GetComponent<Rigidbody>();
        if (rigidbody != null)
        {
            rigidbody.constraints = RigidbodyConstraints.FreezePosition |
                                    RigidbodyConstraints.FreezeRotationX |
                                    RigidbodyConstraints.FreezeRotationZ;
        }
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        // Remove constraints when the object is released
        var rigidbody = GetComponent<Rigidbody>();
        if (rigidbody != null)
        {
            rigidbody.constraints = RigidbodyConstraints.None;
        }

        base.OnSelectExited(args);
    }
}
