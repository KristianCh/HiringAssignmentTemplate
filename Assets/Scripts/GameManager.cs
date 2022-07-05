using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int TowersToSpawn = 3;
    public Tower TowerPrefab;

    // Start is called before the first frame update
    void Start()
    {
        int TotalLevels = 2;
        for (int i = 0; i < TowersToSpawn; i++)
        {
            Tower newTower = Instantiate(TowerPrefab, new Vector3(4 + i * 8, 0, 0), Quaternion.identity);
            newTower.Levels = TotalLevels + 2;
            TotalLevels += newTower.Levels;
            newTower.GenerateTower();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
