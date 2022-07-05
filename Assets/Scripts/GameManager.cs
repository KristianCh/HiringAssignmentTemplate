using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int TowersToSpawn = 3;
    public int TowersGenerated = 0;
    public int TotalLevels = 0;
    public int MinTowerHeight = 4;
    public int MaxTowerHeight = 10;
    public int DestroyedTowers = 0;
    public Tower TowerPrefab;
    public GameGUI Gui;

    public static GameManager Instance;

    public EndGameMenuManager EndGameMenuPrefab;

    // Start is called before the first frame update
    void Start()
    {
        // Set up instance
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
        }
        Instance = this;
    }

    public void Init()
    {
        Room.ResetLevelValues();
        Gui.SetTowersDestroyedText("Towers Destroyed: " + DestroyedTowers.ToString() + "/" + TowersToSpawn.ToString());
        for (int i = 0; i < Mathf.Min(2, TowersToSpawn); i++)
        {
            GenerateTower();
        }
    }

    public void GenerateTower()
    {
        Tower newTower = Instantiate(TowerPrefab, new Vector3(4 + TowersGenerated * 8, 0, 0), Quaternion.identity);
        newTower.Levels = Random.Range(MinTowerHeight, Mathf.Min(TotalLevels + MinTowerHeight, MaxTowerHeight) + 1);
        newTower.transform.SetParent(transform, false);
        TotalLevels += newTower.Levels;
        newTower.GenerateTower();
        TowersGenerated++;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FinishGame(bool success)
    {
        EndGameMenuManager end = Instantiate(EndGameMenuPrefab, Vector3.zero, Quaternion.identity);
        if (success)
        {

        }
        else
        {
            end.TitleText.text = "Game Over";
        }
    }

    public void OnTowerDestroyed(Tower tower)
    {
        PlayerTower.Instance.TargetPosition = tower.transform.position;
        DestroyedTowers++;
        Gui.SetTowersDestroyedText("Towers Destroyed: " + DestroyedTowers.ToString() + "/" + TowersToSpawn.ToString());

        if (TowersGenerated < TowersToSpawn)
        {
            GenerateTower();
        }

        else if (DestroyedTowers == TowersToSpawn)
        {
            FinishGame(true);
        }
    }

    public void OnPlayerDeath()
    {
        FinishGame(false);
    }

    public void DestroyGameLevel()
    {
        Destroy(gameObject);
    }
}
