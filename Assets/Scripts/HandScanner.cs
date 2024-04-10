using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HandScanner : MonoBehaviour
{
    public int HandTrackTime = 5; // Time in seconds to fill the slider
    private float contactTime = 0f; // Time the hand has been in contact
    private bool isHandInContact = false;

    public Slider progressSlider; // Reference to the UI slider

    // Reference to the GameProgression script
    public Game_Progression myGameProgression;

    // Public event
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
        // Check the needToFixShip status
        if (myGameProgression.needToFixShip)
        {
            progressSlider.gameObject.SetActive(true);

            if (isHandInContact && contactTime < HandTrackTime)
            {
                contactTime += Time.deltaTime;
                progressSlider.value = contactTime;

                if (contactTime >= HandTrackTime)
                {
                    ToExecuteHandScanComplete();
                }
            }
        }
        else
        {
            progressSlider.gameObject.SetActive(false);
            isHandInContact = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hands") && myGameProgression.needToFixShip)
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
        }
    }

    public void ToExecuteHandScanComplete()
    {
        ResetSlider();
        ExecuteHandScanComplete.Invoke();
    }

    public void ResetSlider()
    {
        contactTime = 0; // Reset the contact time
        progressSlider.value = 0; // Reset the slider value
    }
}
