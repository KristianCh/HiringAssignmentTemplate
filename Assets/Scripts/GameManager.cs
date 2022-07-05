using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int TowersToSpawn = 3;
    public int MinTowerHeight = 4;
    public Tower TowerPrefab;

    public GameManager Instance;

    // Start is called before the first frame update
    void Start()
    {
        // Set up instance
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
        }
        Instance = this;

        GenerateTowers();
    }

    public void GenerateTowers()
    {
        int TotalLevels = 0;
        for (int i = 0; i < TowersToSpawn; i++)
        {
            Tower newTower = Instantiate(TowerPrefab, new Vector3(4 + i * 8, 0, 0), Quaternion.identity);
            newTower.Levels = Random.Range(MinTowerHeight, TotalLevels + MinTowerHeight + 1);
            TotalLevels += newTower.Levels;
            newTower.GenerateTower();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
