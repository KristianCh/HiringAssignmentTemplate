using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomButton : MonoBehaviour
{
    // Play audio
    public void OnClickBase()
    {
        SoundManager.Instance.PlayButtonAudio();
    }
}
