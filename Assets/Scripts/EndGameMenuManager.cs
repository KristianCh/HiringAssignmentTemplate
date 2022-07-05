using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndGameMenuManager : MonoBehaviour
{
    public GameManager GameLevelPrefab;
    public MainMenuManager MainMenuPrefab;
    public TMP_Text TitleText;
    public TMP_InputField TowersAmountInputField;
    public int CustomTowerAmount = 3;

    public void StartGame()
    {
        SoundManager.Instance.PlayButtonAudio();
        GameManager.Instance.DestroyGameLevel();
        GameManager game = Instantiate(GameLevelPrefab, Vector3.zero, Quaternion.identity);
        game.Init();
        Destroy(gameObject);
    }

    public void StartGameCustom()
    {
        SoundManager.Instance.PlayButtonAudio();
        GameManager.Instance.DestroyGameLevel();
        GameManager game = Instantiate(GameLevelPrefab, Vector3.zero, Quaternion.identity);
        game.TowersToSpawn = CustomTowerAmount;
        game.Init();
        Destroy(gameObject);
    }

    public void ToMainMenu()
    {
        SoundManager.Instance.PlayButtonAudio();
        Instantiate(MainMenuPrefab, Vector3.zero, Quaternion.identity);
        Destroy(gameObject);
    }

    public void OnChangedInputField(string input)
    {
        if (input != "")
        {
            CustomTowerAmount = Int32.Parse(input);
        }
    }
}
