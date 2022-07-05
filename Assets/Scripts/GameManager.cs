using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int TowersToSpawn = 3;
    public int MinTowerHeight = 4;
    private int DestroyedTowers = 0;
    public Tower TowerPrefab;

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
        Room.ResetLevelValues();
    }

    public void GenerateTowers()
    {
        int TotalLevels = 0;
        for (int i = 0; i < TowersToSpawn; i++)
        {
            Tower newTower = Instantiate(TowerPrefab, new Vector3(4 + i * 8, 0, 0), Quaternion.identity);
            newTower.Levels = Random.Range(MinTowerHeight, TotalLevels + MinTowerHeight + 1);
            newTower.transform.SetParent(transform, false);
            TotalLevels += newTower.Levels;
            newTower.GenerateTower();
        }
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
        if (DestroyedTowers == TowersToSpawn)
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
