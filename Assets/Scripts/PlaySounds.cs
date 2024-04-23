using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySounds : MonoBehaviour
{
    public AudioSource soundPlayer;

    [SerializeField] private AudioClip ambientPizzaMusic1;
    [SerializeField] private AudioClip ambientPizzaMusic2;
    [SerializeField] private AudioClip boxSound;
    [SerializeField] private AudioClip buttonClick;
    [SerializeField] private AudioClip communicationNoises;
    [SerializeField] private AudioClip deliveryFailureNotification;
    [SerializeField] private AudioClip droneDeployment1;
    [SerializeField] private AudioClip droneDeployment2;
    [SerializeField] private AudioClip droneArriving;
    [SerializeField] private AudioClip medkitPacked;
    [SerializeField] private AudioClip pickUpDroneNavigator;
    [SerializeField] private AudioClip randomBeepingNoises;
    [SerializeField] private AudioClip shipErrorBuzzThreeTimes;
    [SerializeField] private AudioClip spaceshipThruster;
    [SerializeField] private AudioClip stationChange;
    [SerializeField] private AudioClip systemRebootNoises2;
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
    [SerializeField] private AudioClip Rachel_BootingSystem;

    [SerializeField] private AudioClip NickTent1;
    [SerializeField] private AudioClip NickTent2;
    [SerializeField] private AudioClip NickTent3;
    [SerializeField] private AudioClip NickShipFixed;

    //new zoe sounds
    [SerializeField] private AudioClip openingShipAmbiance;
    [SerializeField] private AudioClip openingLiftOff;

    [SerializeField] private AudioClip shipMalfunctionFixed;
    [SerializeField] private AudioClip gameShipAmbiance;
    [SerializeField] private AudioClip warningMalfunction;
    [SerializeField] private AudioClip handScanner;
    [SerializeField] private AudioClip leverSound1;
    [SerializeField] private AudioClip leverSound2;
    [SerializeField] private AudioClip sparks;
    [SerializeField] private AudioClip steamHisses;
    [SerializeField] private AudioClip droneConnect;

    [SerializeField] private AudioClip mechDoor1;
    [SerializeField] private AudioClip mechDoor2;
    [SerializeField] private AudioClip mechDoor3;

    [SerializeField] private AudioClip thruster;

    void Start()
    {
        soundPlayer = GetComponent<AudioSource>();
    }

    public void PlayAmbientMusic() { soundPlayer.PlayOneShot(ambientPizzaMusic1, 0.3f); }
    public void PlayThruster() { soundPlayer.PlayOneShot(thruster); }
    public void PlayMechDoor3() { soundPlayer.PlayOneShot(mechDoor3); }
    public void PlayMechDoor2() { soundPlayer.PlayOneShot(mechDoor2); }
    public void PlayMechDoor1() { soundPlayer.PlayOneShot(mechDoor1); }
    public void PlayBoxThump() { soundPlayer.PlayOneShot(boxSound); }
    public void PlayDroneConnect() { soundPlayer.PlayOneShot(droneConnect); }
    public void PlaySteamHisses() { soundPlayer.PlayOneShot(steamHisses); }
    public void PlaySparks() { soundPlayer.PlayOneShot(sparks); }
    public void PlayHandScanner() { soundPlayer.PlayOneShot(handScanner); }
    public void PlayWarningMalfunction() { soundPlayer.PlayOneShot(warningMalfunction); }
    public void PlayGameShipAmbiance() { soundPlayer.PlayOneShot(gameShipAmbiance); }
    public void PlayOpeningLiftOff() { soundPlayer.PlayOneShot(openingLiftOff); }

    public void PlayMalfunctionComplete()
    {
        soundPlayer.PlayOneShot(shipMalfunctionFixed);
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
    public void PlayRachelBooting()
    {
        soundPlayer.PlayOneShot(Rachel_BootingSystem);
    }


    public void PlayNickTent1()
    {
        soundPlayer.Stop();
        soundPlayer.PlayOneShot(NickTent1);
    }
    public void PlayNickTent2()
    {
        soundPlayer.PlayOneShot(NickTent2);
    }
    public void PlayNickTent3()
    {
        soundPlayer.PlayOneShot(NickTent3);
    }
    public void PlayNickShipFixed()
    {
        soundPlayer.PlayOneShot(NickShipFixed);
    }
}

