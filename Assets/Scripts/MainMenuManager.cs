using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public GameManager GameLevelPrefab;

    public void StartGame()
    {
        SoundManager.Instance.PlayButtonAudio();
        GameManager game = Instantiate(GameLevelPrefab, Vector3.zero, Quaternion.identity);
        game.Init();
        Destroy(gameObject);
    }

    public void QuitGame()
    {
        SoundManager.Instance.PlayButtonAudio();
        Application.Quit();
    }
}
