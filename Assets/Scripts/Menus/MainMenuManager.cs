using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    // Preset level prefab
    public LevelManager GameLevelPrefab;

    // On start game button click
    public void StartGame()
    {
        // Instantiate level and destroy main menu
        LevelManager game = Instantiate(GameLevelPrefab, Vector3.zero, Quaternion.identity);
        game.Init();
        Destroy(gameObject);
    }

    // On quit game button click
    public void QuitGame()
    {
        // Play sound, close app
        Application.Quit();
    }
}
