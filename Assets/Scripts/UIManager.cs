using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
    public GameObject DialogueBox;
    public GameObject OrderBox;
    public GameObject TutorialBox;
    public GameObject DeliveryResultBox;

    public float delayedTime; // Time in seconds to wait before disabling the Dialogue Box

    public UnityEvent EnableDropSwitchTip;

    public UnityEvent EnableAfterDropTip;

    public bool isFirstRelease = false;
    void Update()
    {
    }

    public void DialogueBoxOn() => DialogueBox.SetActive(true);
    public void DialogueBoxOff() => DialogueBox.SetActive(false);

    // New function to disable the Dialogue Box after a delay
    public void DialogueBoxDelayedOff()
    {
        StartCoroutine(DisableAfterDelay(DialogueBox, delayedTime));
    }

    private IEnumerator DisableAfterDelay(GameObject gameObject, float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }

    public void OrderBoxOn() => OrderBox.SetActive(true);
    public void OrderBoxOff() => OrderBox.SetActive(false);

    public void TutorialBoxOn() => TutorialBox.SetActive(true);
    public void TutorialBoxOff() => TutorialBox.SetActive(false);

    public void DeliveryResultBoxOn() => DeliveryResultBox.SetActive(true);
    public void DeliveryResultBoxOff() => DeliveryResultBox.SetActive(false);

    public void ToEnableDropSwitchTip()
    {
        EnableDropSwitchTip.Invoke();
    }

    public void ToEnableAfterDropTip()
    {
        if(isFirstRelease == false)
        {
            EnableAfterDropTip.Invoke();
            isFirstRelease = true;
        }
        else
        {
            return ;
        }
    }

}