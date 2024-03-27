using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System.Net;

public class UIManager : MonoBehaviour
{
    public GameObject DialogueBox;
    public GameObject OrderBox;
    //public GameObject TutorialBox;
    //public GameObject DeliveryResultBox;

    public GameObject TutorialArea;
    public GameObject SuccessArea;
    public GameObject FailArea;

    public float delayedTime; // Time in seconds to wait before disabling the Dialogue Box
    public float tutorialBackDelayedTIme; //Time to wait before swtiching the tutorial screen back on

    public UnityEvent EnableDropSwitchTip;

    public UnityEvent EnableAfterDropTip;

    public UnityEvent SuccessDeliver;
    public UnityEvent FailDeliver;

    public bool isFirstRelease = false;
    void Update()
    {
    }

    // Codes for delay switching the bottom right screen to tutorial
    void Start()
    {
        // Assign the methods to the UnityEvents
        SuccessDeliver.AddListener(OnSuccessDelivery);
        FailDeliver.AddListener(OnFailDelivery);
    }

    public void OnSuccessDelivery()
    {
        StartCoroutine(DelayedAreaManagement());
    }

    public void OnFailDelivery()
    {
        StartCoroutine(DelayedAreaManagement());
    }

    private IEnumerator DelayedAreaManagement()
    {
        yield return new WaitForSeconds(tutorialBackDelayedTIme);
        TutorialAreaOn();
        SuccessAreaOff();
        FailAreaOff();
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

    //public void TutorialBoxOn() => TutorialBox.SetActive(true);
    //public void TutorialBoxOff() => TutorialBox.SetActive(false);

    //public void DeliveryResultBoxOn() => DeliveryResultBox.SetActive(true);
    //public void DeliveryResultBoxOff() => DeliveryResultBox.SetActive(false);

    // Methods for TutorialArea
    public void TutorialAreaOn() => TutorialArea.SetActive(true);
    public void TutorialAreaOff() => TutorialArea.SetActive(false);

    // Methods for SuccessArea
    public void SuccessAreaOn() => SuccessArea.SetActive(true);
    public void SuccessAreaOff() => SuccessArea.SetActive(false);

    // Methods for FailArea
    public void FailAreaOn() => FailArea.SetActive(true);
    public void FailAreaOff() => FailArea.SetActive(false);
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

    public void ToSuccessDelivery()
    {
        SuccessDeliver.Invoke();
    }

    public void ToFailDelivery()
    {
        FailDeliver.Invoke();
    }

}