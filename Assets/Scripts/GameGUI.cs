using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameGUI : MonoBehaviour
{
    public TMP_Text TowersDestroyedText;

    public void SetTowersDestroyedText(string text)
    {
        TowersDestroyedText.text = text;
    }
}
