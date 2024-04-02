using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System.Net;
using Unity.VisualScripting;

public class UIManager : MonoBehaviour
{
    public GameObject DialogueBox;
    public GameObject OrderBox;
    //public GameObject TutorialBox;
    //public GameObject DeliveryResultBox;

    public GameObject TutorialArea;
    public GameObject SuccessArea;
    public GameObject FailArea;

    public GameObject mySwitch;

    public float delayedTime; // Time in seconds to wait before disabling the Dialogue Box
    public float tutorialBackDelayedTIme; //Time to wait before swtiching the tutorial screen back on

    public UnityEvent EnableDropSwitchTip;

    public UnityEvent EnableAfterDropTip;

    public UnityEvent SuccessDeliver;
    public UnityEvent FailDeliver;

    public UnityEvent ResetDialogue;

    public UnityEvent ChangeSwitchScreenBack;

    public bool isFirstRelease = false;

    //Current station info
    public int currentStationNum;
    private Rotate_Me_Parent stationController;
    void Update()
    {
        //Get newest current station number
        if (stationController != null)
        {
            currentStationNum = stationController.moveValue;
        }
    }

    // Codes for delay switching the bottom right screen to tutorial
    void Start()
    {
        //Current Station
        GameObject STATIONS_MOVABLE = GameObject.Find("STATIONS_MOVABLE");
        if (STATIONS_MOVABLE != null)
        {
            stationController = STATIONS_MOVABLE.GetComponent<Rotate_Me_Parent>();
        }
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
        yield return new WaitForSeconds(delayedTime);
        TutorialAreaOn();
        SuccessAreaOff();
        FailAreaOff();
    }


    public void DialogueBoxOn() => DialogueBox.SetActive(true);
    public void DialogueBoxOff() => DialogueBox.SetActive(false);

    // New function to disable the Dialogue Box after a delay
    public void DialogueBoxDelayedOff()
    {
        if (currentStationNum == 1 )
        {
            StartCoroutine(DisableAfterDelay(DialogueBox, delayedTime));
        }
        else
        {
            Debug.Log("DialogueBoxDelayedOff called, but currentStationNum is not 1. Action not performed.");
        }
    }

    private IEnumerator DisableAfterDelay(GameObject gameObject, float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }

    public void OrderBoxOn()
    {
        if (currentStationNum == 1)
        {
            OrderBox.SetActive(true);
        }
        else
        {
            Debug.Log("OrderBoxOn called, but currentStationNum is not 1. Action not performed.");
        }
    }
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
        StartCoroutine(WaitAndChangeScreen(3f));
    }

    public void ToFailDelivery()
    {
        FailDeliver.Invoke();
        StartCoroutine(WaitAndChangeScreen(3f));
    }

    public void ToResetDialogue()
    {
        ResetDialogue.Invoke();
    }


    public void ToChangeSwitchScreenBack()
    {
        ChangeSwitchScreenBack.Invoke();
    }

    IEnumerator WaitAndChangeScreen(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        ToChangeSwitchScreenBack();
    }
}