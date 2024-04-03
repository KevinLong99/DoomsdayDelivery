using UnityEngine;

using UnityEngine;
using UnityEngine.UI;

public class HandScanner : MonoBehaviour
{
    public int HandTrackTime = 5; // Time in seconds to fill the slider
    private float contactTime = 0f; // Time the hand has been in contact
    private bool isHandInContact = false;

    public Slider progressSlider; // Reference to the UI slider

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
        if (isHandInContact)
        {
            contactTime += Time.deltaTime;
            progressSlider.value = contactTime;

            if (contactTime >= HandTrackTime)
            {
                TriggerFunction();
                contactTime = 0; // Reset the contact time
                progressSlider.value = 0; // Reset the slider
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
            contactTime = 0; // Reset the contact time on exit
            progressSlider.value = 0; // Reset the slider
        }
    }

    private void TriggerFunction()
    {
        // Placeholder function triggered when the slider is full
        Debug.Log("Hand has been in contact for the required time.");
    }
}