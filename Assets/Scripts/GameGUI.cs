using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameGUI : MonoBehaviour
{
    // Text showing number of destroyed towers
    public TMP_Text TowersDestroyedText;

    // Set text
    public void SetTowersDestroyedText(string text)
    {
        TowersDestroyedText.text = text;
    }
}
