using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class LeverReleaseDetector : MonoBehaviour
{
    public UnityEvent onLeverRelease;

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
    }
}
