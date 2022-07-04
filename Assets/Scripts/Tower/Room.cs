using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public List<RoomEntity> RoomEntities = new List<RoomEntity>();
    public Tower ParentTower;
    public int Level = 1;
    public bool RemoveOnEmpty = true;

    public Enemy EnemyPrefab;

    private static int LastLevelMaxValue = 8;
    private static int TotalEnemyValues = 8;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InteractWithRoom()
    {
        if (RoomEntities.Count > 0)
        {
            FightRoomEntities();
        }
        else if (Player.Instance != null)
        {
            Player.Instance.MoveToRoom(this);
        }
    }

    public void FightRoomEntities()
    {
        bool playerWon = true;
        for (int i = 0; i < RoomEntities.Count; i++)
        {
            playerWon = RoomEntities[i].CompareRoomEntity(Player.Instance);
            if (!playerWon)
            {
                break;
            }
            
        }
        if (playerWon && RemoveOnEmpty)
        {
            ParentTower.RemoveRoom(Level);
            Destroy(gameObject);
        }
    }

    public void AddRoomEntity(RoomEntity roomEntity)
    {
        RoomEntities.Add(roomEntity);
        roomEntity.ParentRoom = this;
        roomEntity.transform.SetParent(this.transform, false);
    }

    public void RemoveRoomEntity(RoomEntity roomEntity)
    {
        RoomEntities.Remove(roomEntity);
    }

    public void PopulateRoom(int difficulty)
    {
        int enemiesToSpawn = Random.Range(1, 3);

        int highestLevelValue = 0;
        for (int i = enemiesToSpawn - 1; i >= 0; i--)
        {
            Enemy newEnemy = Instantiate(EnemyPrefab, new Vector3(1.5f - i * 1.5f, 0, 0), Quaternion.identity);
            newEnemy.ParentRoom = this;
            newEnemy.transform.SetParent(this.transform, false);
            AddRoomEntity(newEnemy);

            int value = (int)Mathf.Min(TotalEnemyValues, Mathf.Round(LastLevelMaxValue * 2f + Random.Range(-LastLevelMaxValue, 0)));
            newEnemy.SetValue(value);
            TotalEnemyValues += value;
            if (value > highestLevelValue) 
            { 
                highestLevelValue = value; 
            }
        }
        LastLevelMaxValue = highestLevelValue;
    }
}
