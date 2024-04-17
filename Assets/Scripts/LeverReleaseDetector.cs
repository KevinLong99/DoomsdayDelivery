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
        //Debug.Log("enable");
        interactable.selectExited.AddListener(HandleSelectExited);
    }

    void OnDisable()
    {
        //Debug.Log("disable");
        interactable.selectExited.RemoveListener(HandleSelectExited);
    }

    private void HandleSelectExited(SelectExitEventArgs arg)
    {
        
        onLeverRelease?.Invoke();
    }
}
