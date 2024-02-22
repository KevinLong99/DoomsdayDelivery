using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LeverGrabListener : XRGrabInteractable
{
    private LeverReturn leverReturnScript;

    protected override void OnEnable()
    {
        base.OnEnable();
        Debug.Log("LeverGrabListener override ENABLE");
        leverReturnScript = GetComponent<LeverReturn>();
        selectExited.AddListener(OnLeverReleased);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        Debug.Log("LeverGrabListener override DISABLE");
        selectExited.RemoveListener(OnLeverReleased);
    }

    private void OnLeverReleased(SelectExitEventArgs args)
    {
        // Call the ReleaseLever function on the LeverReturn script
        leverReturnScript.ReleaseLever();
    }
}