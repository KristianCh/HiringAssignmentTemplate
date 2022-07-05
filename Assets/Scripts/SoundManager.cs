using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Audio sources
    public AudioSource FightAudio;
    public AudioSource PickupAudio;
    public AudioSource JumpAudio;
    public AudioSource ButtonAudio;

    public static SoundManager Instance;

    public void Start()
    {
        // Set up instance
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    // Play fight audio
    public void PlayFightAudio()
    {
        FightAudio.Play(0);
    }

    // Play item pick up audio
    public void PlayPickupAudio()
    {
        PickupAudio.Play(0);
    }

    // Play jump audio
    public void PlayJumpAudio()
    {
        JumpAudio.Play(0);
    }

    // Play button audio
    public void PlayButtonAudio()
    {
        ButtonAudio.Play(0);
    }
}
