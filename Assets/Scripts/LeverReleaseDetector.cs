using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class LeverReleaseDetector : MonoBehaviour
{
    public UnityEvent onLeverRelease;
    public UnityEvent onPullError;
    public ConfigurableJointListener myJointListener; // Add a reference to the ConfigurableJointListener

    private XRBaseInteractable interactable;

    void Awake()
    {
        interactable = GetComponent<XRBaseInteractable>();
    }

    void OnEnable()
    {
        interactable.selectExited.AddListener(HandleSelectExited);
    }

    void OnDisable()
    {
        interactable.selectExited.RemoveListener(HandleSelectExited);
    }

    private void HandleSelectExited(SelectExitEventArgs arg)
    {
        onLeverRelease?.Invoke();
        CheckJointListenerStatus();
    }

    private void CheckJointListenerStatus()
    {
        // Check if myJointListener is not enabled and call OnLeverPullError if it is not
        if (myJointListener != null && !myJointListener.enabled)
        {
            OnLeverPullError();
        }
    }

    private void OnLeverPullError()
    {
        // Handle the error when the joint listener is not enabled upon lever release
        onPullError?.Invoke();
        Debug.LogError("Lever was pulled incorrectly or at an inappropriate time.");
    }
}
