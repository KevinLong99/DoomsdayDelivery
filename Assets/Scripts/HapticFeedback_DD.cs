using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR.Interaction.Toolkit;

public class HapticFeedback_DD : MonoBehaviour
{
    public XRBaseController rightController;
    public XRBaseController leftController;

    public static HapticFeedback_DD instance;
    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("Multiple haptic event singletons");
            Destroy(this);
        }
    }

    public void RightControllerHapticEvent(float intensity, float duration) { HapticEvent(rightController, intensity, duration); }
    public void LefttControllerHapticEvent(float intensity, float duration) { HapticEvent(leftController, intensity, duration); }

    public void HapticEvent(XRBaseController controller, float intensity, float duration)
    {
        if (intensity > 0)
            controller.SendHapticImpulse(intensity, duration);
    }

    public void StandardHapticShock()
    {
        rightController.SendHapticImpulse(1f, 1f);
        leftController.SendHapticImpulse(1f, 1f);
    }
}

//referenced from our team's old project: Fossil Finders
//to call it:
//HapticFeedback_DD.instance.RightControllerHapticEvent(1f, .1f);
