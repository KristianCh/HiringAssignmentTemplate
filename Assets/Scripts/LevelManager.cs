using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int TowersToSpawn = 3;
    public int TowersGenerated = 0;
    public int TotalLevels = 0;
    public int MinTowerHeight = 4;
    public int MaxTowerHeight = 10;
    public int DestroyedTowers = 0;
    public int[] TowerHeights = new int[0];
    public Tower TowerPrefab;
    public GameGUI Gui;

    public static LevelManager Instance;
    public GameLevelScriptableObject GameLevelData;

    public EndGameMenuManager EndGameMenuPrefab;

    // Start is called before the first frame update
    void Start()
    {
        // Set up player instance
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
        }
        Instance = this;
    }

    // Initialize values for tower generation, generate first two towers
    public void Init()
    {
        Room.ResetLevelValues();
        // Load data from scriptable object
        if (GameLevelData != null)
        {
            if (GameLevelData.UseTowerCount) TowersToSpawn = GameLevelData.TowersToSpawn;
            MinTowerHeight = GameLevelData.MinTowerHeight;
            MaxTowerHeight = GameLevelData.MaxTowerHeight;
            TowerHeights = GameLevelData.TowerHeights;
        }
        Gui.SetTowersDestroyedText("Towers Destroyed: " + DestroyedTowers.ToString() + "/" + TowersToSpawn.ToString());
        // Generate first 2 towers
        for (int i = 0; i < Mathf.Min(2, TowersToSpawn); i++)
        {
            GenerateTower();
        }
    }

    // Generates a new tower
    public void GenerateTower()
    {
        Tower newTower = Instantiate(TowerPrefab, new Vector3(4 + TowersGenerated * 8, 0, 0), Quaternion.identity);
        if (TowersGenerated < TowerHeights.Length)
        {
            newTower.Levels = TowerHeights[TowersGenerated];
        }
        else
        {
            newTower.Levels = Random.Range(MinTowerHeight, Mathf.Min(TotalLevels + MinTowerHeight, MaxTowerHeight) + 1);
        }
        newTower.transform.SetParent(transform, false);
        TotalLevels += newTower.Levels;
        newTower.GenerateTower();
        TowersGenerated++;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Handle end of game
    public void FinishGame(bool success)
    {
        EndGameMenuManager end = Instantiate(EndGameMenuPrefab, Vector3.zero, Quaternion.identity);
        // Set appropriate end text
        if (!success)
        {
            end.TitleText.text = "Game Over";
        }
    }

    // When a tower is destroyed
    public void OnTowerDestroyed(Tower tower)
    {
        // Update values
        PlayerTower.Instance.TargetPosition = tower.transform.position;
        DestroyedTowers++;
        Gui.SetTowersDestroyedText("Towers Destroyed: " + DestroyedTowers.ToString() + "/" + TowersToSpawn.ToString());

        // If not all towers have been spawned yet, generate another tower
        if (TowersGenerated < TowersToSpawn)
        {
            GenerateTower();
        }
        // If all towers where destroyed, end game
        else if (DestroyedTowers == TowersToSpawn)
        {
            FinishGame(true);
        }
    }

    // Called by player when player dies
    public void OnPlayerDeath()
    {
        FinishGame(false);
    }

    // Destroy level
    public void DestroyGameLevel()
    {
        Destroy(gameObject);
    }
}
