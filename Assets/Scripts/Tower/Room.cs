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
        for (int i = RoomEntities.Count - 1; i >= 0; i--)
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

    public void OnMouseUp()
    {
        if (Player.Instance != null)
        {
            InteractWithRoom();
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
        Debug.Log(enemiesToSpawn);

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Enemy newEnemy = Instantiate(EnemyPrefab, new Vector3(1.5f - i * 1.5f, 0, 0), Quaternion.identity);
            int baseValue = 4 + (int) Mathf.Pow(2, Level + difficulty * 5);
            newEnemy.SetValue(
                    (int) Mathf.Round(baseValue + Random.Range(-baseValue / 4f, baseValue / 4f))
                );

            newEnemy.ParentRoom = this;
            newEnemy.transform.SetParent(this.transform, false);

            AddRoomEntity(newEnemy);

        }
    }
}
