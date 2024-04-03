using UnityEngine;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HandScanner : MonoBehaviour
{
    public int HandTrackTime = 5; // Time in seconds to fill the slider
    private float contactTime = 0f; // Time the hand has been in contact
    private bool isHandInContact = false;

    public Slider progressSlider; // Reference to the UI slider

    //Public event
    public UnityEvent ExecuteHandScanComplete;

    private void Start()
    {
        if (progressSlider == null)
        {
            Debug.LogError("Progress Slider is not assigned in the inspector.");
            return;
        }

        progressSlider.minValue = 0;
        progressSlider.maxValue = HandTrackTime;
        progressSlider.value = 0;
    }

    private void Update()
    {
        if (isHandInContact && contactTime < HandTrackTime)
        {
            contactTime += Time.deltaTime;
            progressSlider.value = contactTime;

            if (contactTime >= HandTrackTime)
            {
                ToExecuteHandScanComplete();
                // Removed resetting of contactTime and progressSlider.value here to keep the slider full
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hands"))
        {
            isHandInContact = true;
            contactTime = 0; // Reset the contact time on new contact
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hands"))
        {
            isHandInContact = false;
            // Removed resetting of contactTime and progressSlider.value here to keep the slider full on exit
        }
    }

    public void ToExecuteHandScanComplete()
    {
        ExecuteHandScanComplete.Invoke();
    }

    public void ResetSlider()
    {
        contactTime = 0; // Reset the contact time
        progressSlider.value = 0; // Reset the slider value
        progressSlider.gameObject.SetActive(false); // Temporarily disable the slider UI GameObject
    }
}