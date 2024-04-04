using UnityEngine;
using UnityEngine.UI;

public class KnobWheel : MonoBehaviour
{
    public Slider progressSlider;
    private float initialRotation;
    private float currentRotation;
    private float rotationProgress;
    public float requiredRotation = 360f; // Amount of rotation needed to fill the slider

    private void Start()
    {
        if (progressSlider == null)
        {
            Debug.LogError("Progress Slider is not assigned in the inspector.");
            return;
        }

        progressSlider.minValue = 0;
        progressSlider.maxValue = requiredRotation;
        progressSlider.value = 0;

        initialRotation = GetCurrentRotationZ();
        rotationProgress = 0;
    }

    private void Update()
    {
        currentRotation = GetCurrentRotationZ() - initialRotation;
        rotationProgress = Mathf.Clamp(rotationProgress + currentRotation, 0, requiredRotation);
        progressSlider.value = rotationProgress;

        if (rotationProgress >= requiredRotation)
        {
            TriggerFunction();
            // Optional: Reset if you want to start over
            // ResetRotationProgress();
        }

        initialRotation = GetCurrentRotationZ();
    }

    private float GetCurrentRotationZ()
    {
        return transform.localEulerAngles.z;
    }

    private void TriggerFunction()
    {
        Debug.Log("Wheel has been turned the required distance.");
    }

    public void ResetRotationProgress()
    {
        rotationProgress = 0;
        progressSlider.value = 0;
        initialRotation = GetCurrentRotationZ();
    }
}