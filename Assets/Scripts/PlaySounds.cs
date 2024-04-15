using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySounds : MonoBehaviour
{
    AudioSource soundPlayer;

    [SerializeField] private AudioClip ambientPizzaMusic1;
    [SerializeField] private AudioClip ambientPizzaMusic2;
    [SerializeField] private AudioClip boingPoing;
    [SerializeField] private AudioClip boxSound;
    [SerializeField] private AudioClip buttonClick;
    [SerializeField] private AudioClip communicationNoises;
    [SerializeField] private AudioClip deliveryFailureNotification;
    [SerializeField] private AudioClip droneDeployment1;
    [SerializeField] private AudioClip droneDeployment2;
    [SerializeField] private AudioClip droneArriving;
    [SerializeField] private AudioClip medkitPacked;
    [SerializeField] private AudioClip newOrderNotification;
    [SerializeField] private AudioClip pickUpDroneNavigator;
    [SerializeField] private AudioClip randomBeepingNoises;
    [SerializeField] private AudioClip shipErrorBuzzThreeTimes;
    [SerializeField] private AudioClip spaceshipThruster;
    [SerializeField] private AudioClip stationChange;
    [SerializeField] private AudioClip systemRebootNoises2;
    [SerializeField] private AudioClip tacoBellGong;
    [SerializeField] private AudioClip thunder;
    [SerializeField] private AudioClip timeGasNotification;
    [SerializeField] private AudioClip tutorialNotification;

    [SerializeField] private AudioClip Ana_Fail;
    [SerializeField] private AudioClip Ana_Success;
    [SerializeField] private AudioClip JJ_Fail;
    [SerializeField] private AudioClip JJ_Success;
    [SerializeField] private AudioClip Nick_Fail;
    [SerializeField] private AudioClip Nick_Intro;
    [SerializeField] private AudioClip Nick_OneMinuteWarning;
    [SerializeField] private AudioClip Nick_Success;
    [SerializeField] private AudioClip Rachel_Fail;
    [SerializeField] private AudioClip Rachel_Success;

    void Start()
    {
        soundPlayer = GetComponent<AudioSource>();
    }

    public void PlayChosenAudioClip(AudioClip chosenAudioClip)
    {
        soundPlayer.PlayOneShot(chosenAudioClip);
    }

    public void PlayRandomBeeping()
    {
        soundPlayer.PlayOneShot(randomBeepingNoises);
    }

    public void PlayErorr()
    {
        soundPlayer.PlayOneShot(shipErrorBuzzThreeTimes);
    }

    public void PlayAnaFail()
    {
        soundPlayer.PlayOneShot(Ana_Fail);
    }
    public void PlayAnaSuccess()
    {
        soundPlayer.PlayOneShot(Ana_Success);
    }
    public void PlayJJFail()
    {
        soundPlayer.PlayOneShot(JJ_Fail);
    }
    public void PlayJJSuccess()
    {
        soundPlayer.PlayOneShot(JJ_Success);
    }
    public void PlayNickFail()
    {
        soundPlayer.PlayOneShot(Nick_Fail);
    }
    public void PlayNickIntro()
    {
        soundPlayer.clip = null;
    }
    public void PlayNickWarning()
    {
        soundPlayer.PlayOneShot(Nick_OneMinuteWarning);
    }
    public void PlayNickSuccess()
    {
        soundPlayer.PlayOneShot(Nick_Success);
    }
    public void PlayRachelFail()
    {
        soundPlayer.PlayOneShot(Rachel_Fail);
    }
    public void PlayRachelSuccess()
    {
        soundPlayer.PlayOneShot(Rachel_Success);
    }
}

