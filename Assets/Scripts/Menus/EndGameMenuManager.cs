using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndGameMenuManager : MonoBehaviour
{
    // Generated level prefab
    public LevelManager GameLevelPrefab;
    // Preset level prefab
    public LevelManager PresetGameLevelPrefab;
    // Main Menu prefab
    public MainMenuManager MainMenuPrefab;
    public TMP_Text TitleText;
    public TMP_InputField TowersAmountInputField;
    // Amount of towers to create in generated level
    public int CustomTowerAmount = 3;

    // On start game button click
    public void StartGame()
    {
        // Instantiate level and destroy end menu
        LevelManager.Instance.DestroyGameLevel();
        LevelManager level = Instantiate(PresetGameLevelPrefab, Vector3.zero, Quaternion.identity);
        level.Init();
        Destroy(gameObject);
    }

    // On start generated game button click
    public void StartGameCustom()
    {
        // Instantiate and set up level and destroy end menu
        LevelManager.Instance.DestroyGameLevel();
        LevelManager level = Instantiate(GameLevelPrefab, Vector3.zero, Quaternion.identity);
        level.TowersToSpawn = CustomTowerAmount;
        level.Init();
        Destroy(gameObject);
    }

    // On to main menu button
    public void ToMainMenu()
    {
        // Instantiate main menu and destroy end menu
        LevelManager.Instance.DestroyGameLevel();
        Instantiate(MainMenuPrefab, Vector3.zero, Quaternion.identity);
        Destroy(gameObject);
    }

    // Update custom tower amount when value in input field changes
    public void OnChangedInputField(string input)
    {
        if (input != "")
        {
            CustomTowerAmount = Int32.Parse(input);
        }
    }
}
