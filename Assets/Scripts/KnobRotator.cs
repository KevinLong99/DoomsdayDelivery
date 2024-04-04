using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class KnobRotator : MonoBehaviour
{
    public XRGrabInteractable grabInteractable;
    public Slider progressSlider; // Assign this in the inspector
    public Transform wheel; // Assign the wheel transform here in the inspector

    private Quaternion initialGrabRotation;
    private float totalRotation = 0f; // Total rotation since the grab started
    private bool isGrabbed = false; // To track the grab state

    void Awake()
    {
        grabInteractable.selectEntered.AddListener(OnGrabbed);
        grabInteractable.selectExited.AddListener(OnReleased);

        if (progressSlider == null)
        {
            Debug.LogError("Progress Slider is not assigned in the inspector.");
        }
        else
        {
            progressSlider.minValue = 0;
            progressSlider.maxValue = 360f; // Set to the desired maximum rotation
            progressSlider.value = 0;
        }
    }

    private void OnGrabbed(SelectEnterEventArgs arg)
    {
        isGrabbed = true;
        initialGrabRotation = transform.localRotation;
    }

    private void OnReleased(SelectExitEventArgs arg)
    {
        isGrabbed = false;
    }

    void Update()
    {
        if (isGrabbed)
        {
            Quaternion currentRotation = transform.localRotation;
            Quaternion deltaRotation = Quaternion.Inverse(initialGrabRotation) * currentRotation;

            float deltaAngle = deltaRotation.eulerAngles.y;
            if (deltaAngle > 180f) deltaAngle -= 360f;

            totalRotation += deltaAngle;

            if (wheel != null)
            {
                // Apply the rotation delta to the wheel
                wheel.Rotate(0, deltaAngle, 0, Space.Self);
            }

            // Update the initial rotation for the next calculation
            initialGrabRotation = currentRotation;

            // Update the slider's value based on the total rotation
            progressSlider.value = Mathf.Clamp(totalRotation % 360f, 0, 360f);
        }
    }
}
