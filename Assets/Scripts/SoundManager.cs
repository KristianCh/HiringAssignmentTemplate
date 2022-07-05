using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public AudioSource FightAudio;
    public AudioSource PickupAudio;
    public AudioSource JumpAudio;
    public AudioSource ButtonAudio;

    public void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayFightAudio()
    {
        FightAudio.Play(0);
    }

    public void PlayPickupAudio()
    {
        PickupAudio.Play(0);
    }

    public void PlayJumpAudio()
    {
        JumpAudio.Play(0);
    }

    public void PlayButtonAudio()
    {
        ButtonAudio.Play(0);
    }
}
