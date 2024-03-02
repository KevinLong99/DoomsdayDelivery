using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySounds : MonoBehaviour
{
    AudioSource soundPlayer;

    [SerializeField] private AudioClip nineSecondShipFly;
    [SerializeField] private AudioClip aoogahError;
    [SerializeField] private AudioClip backInTheDay;
    [SerializeField] private AudioClip boingPoing;
    [SerializeField] private AudioClip droneArriving;
    [SerializeField] private AudioClip fnaf1;
    [SerializeField] private AudioClip minecraftCave1;
    [SerializeField] private AudioClip shipError3Buzz;
    [SerializeField] private AudioClip tacoBellGong;
    [SerializeField] private AudioClip thunder;
    [SerializeField] private AudioClip thunk;

    void Start()
    {
        soundPlayer = GetComponent<AudioSource>();
    }

    public void PlayNineSecondsShipFly()
    {
        soundPlayer.PlayOneShot(nineSecondShipFly);
    }
    public void PlayAoogahError()
    {
        soundPlayer.PlayOneShot(aoogahError);
    }

    public void PlayBackInTheDay()
    {
        soundPlayer.PlayOneShot(backInTheDay);
    }
    public void PlayBoingPoing()
    {
        soundPlayer.PlayOneShot(boingPoing);
    }
    public void PlayDroneArriving()
    {
        soundPlayer.PlayOneShot(droneArriving);
    }
    public void PlayFNAF1()
    {
        soundPlayer.PlayOneShot(fnaf1);
    }
    public void PlayMinecraftCave1()
    {
        soundPlayer.PlayOneShot(minecraftCave1);
    }
    public void PlayShipError3Buzz()
    {
        soundPlayer.PlayOneShot(shipError3Buzz);
    }
    public void PlayTacoBellGong()
    {
        soundPlayer.PlayOneShot(tacoBellGong);
    }
    public void PlayThunder()
    {
        soundPlayer.PlayOneShot(thunder);
    }
    public void PlayThunk()
    {
        soundPlayer.PlayOneShot(thunk);
    }
}
