using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Level")]
public class GameLevelScriptableObject : ScriptableObject
{
    public int TowersToSpawn;
    public bool UseTowerCount;
    public int MinTowerHeight;
    public int MaxTowerHeight;

    public int[] TowerHeights;
}
