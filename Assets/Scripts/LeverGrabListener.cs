using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LeverGrabListener : XRGrabInteractable
{
    private LeverReturn leverReturnScript;

    protected override void OnEnable()
    {
        base.OnEnable();
        leverReturnScript = GetComponent<LeverReturn>();
        selectExited.AddListener(OnLeverReleased);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        selectExited.RemoveListener(OnLeverReleased);
    }

    private void OnLeverReleased(SelectExitEventArgs args)
    {
        // Call the ReleaseLever function on the LeverReturn script
        leverReturnScript.ReleaseLever();
    }
}