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
}
