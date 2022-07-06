using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // Towers to spawn in this level
    public int TowersToGenerate = 3;
    // Total towers generated so far
    public int TowersGenerated = 0;
    // Total rooms of towers generated (rooms)
    public int TotalRooms = 0;
    // Min and max tower height
    public int MinTowerHeight = 4;
    public int MaxTowerHeight = 10;
    // Total destroyed towers so far
    public int DestroyedTowers = 0;
    // Predefined tower heights overriding random generation
    public int[] TowerHeights = new int[0];
    // Tower prefab
    public Tower TowerPrefab;
    // Gui game object
    public GameGUI Gui;

    // Instance
    public static LevelManager Instance;
    // Level data for generation
    public GameLevelScriptableObject GameLevelData;

    // End menu prefab
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

    // Initialize values for tower generation, generate first two towers
    public void Init()
    {
        Room.ResetRoomValues();
        // Load data from scriptable object
        if (GameLevelData != null)
        {
            if (GameLevelData.UseTowerCount) TowersToGenerate = GameLevelData.TowersToGenerate;
            MinTowerHeight = GameLevelData.MinTowerHeight;
            MaxTowerHeight = GameLevelData.MaxTowerHeight;
            TowerHeights = GameLevelData.TowerHeights;
        }
        Gui.SetTowersDestroyedText("Towers Destroyed: " + DestroyedTowers.ToString() + "/" + TowersToGenerate.ToString());
        // Generate first 2 towers
        for (int i = 0; i < Mathf.Min(2, TowersToGenerate); i++)
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
            newTower.Rooms = TowerHeights[TowersGenerated];
        }
        else
        {
            newTower.Rooms = Random.Range(MinTowerHeight, Mathf.Min(TotalRooms + MinTowerHeight, MaxTowerHeight) + 1);
        }
        newTower.transform.SetParent(transform, false);
        TotalRooms += newTower.Rooms;
        newTower.GenerateTower();
        TowersGenerated++;
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
        Gui.SetTowersDestroyedText("Towers Destroyed: " + DestroyedTowers.ToString() + "/" + TowersToGenerate.ToString());

        // If not all towers have been spawned yet, generate another tower
        if (TowersGenerated < TowersToGenerate)
        {
            GenerateTower();
        }
        // If all towers where destroyed, end game
        else if (DestroyedTowers == TowersToGenerate)
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
