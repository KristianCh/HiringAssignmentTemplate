using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public Vector3 TargetPosition = new Vector3(0, 2.4f, 0);
    // List of room entities in room
    public List<RoomEntity> RoomEntities = new List<RoomEntity>();
    // Tower containing this room
    public Tower ParentTower;
    // Level of room
    /*
     * Remove room when it is empty
     * Should be true for enemy tower and false for player tower
     */
    public bool RemoveOnEmpty = true;

    // Enemy prefab
    public Enemy EnemyPrefab;

    // Values of previous enemy and all enemies together
    private static int LastLevelMaxValue = 8;
    private static int TotalEnemyValues = 8;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, TargetPosition, Time.deltaTime * 10);
    }

    /*
     * Player interaction with room
     * If room is empty, player will move to it. Empty rooms should only be in the player tower
     * If room is not empty, player will fight enemies or pick up items
     */
    public void InteractWithRoom()
    {
        if (RoomEntities.Count > 0)
        {
            // Begin attack movement to first room entity
            Player.Instance.BeginAttackMovement(RoomEntities[0]);
            Player.onFinishMovement += OnAttackMovementComplete;
        }
        else if (Player.Instance != null)
        {
            // Move to player empty room
            Player.Instance.MoveToRoom(this);
        }
    }

    //Processes completed attack
    public void OnAttackMovementComplete()
    {
        Player.onFinishMovement = null;
        // Compare room entities, get if player won
        bool playerWon = RoomEntities[0].CompareRoomEntity(Player.Instance);
        // If player lost return
        if (!playerWon)
        {
            return;
        }
        // If room is empty, return player to starting room
        if (RoomEntities.Count == 0)
        {
            Player.Instance.MoveToRoom(Player.Instance.ParentRoom);
            // If room should be removed, add on return to room to on movement finished
            if (RemoveOnEmpty)
            {
                Player.onFinishMovement += OnReturnToRoom;
            }
        }
        else
        {
            // Begin attack on next entity
            Player.Instance.BeginAttackMovement(RoomEntities[0]);
            Player.onFinishMovement += OnAttackMovementComplete;
        }
    }

    // When player returns to starting room, remove empty room
    public void OnReturnToRoom()
    {
        Player.onFinishMovement = null;
        ParentTower.RemoveRoom(this);
        Destroy(gameObject);
    }

    // Add a room entity to the room
    public void AddRoomEntity(RoomEntity roomEntity)
    {
        RoomEntities.Add(roomEntity);
        roomEntity.ParentRoom = this;
        roomEntity.transform.SetParent(this.transform, false);
    }

    // Removes room entity from room
    public void RemoveRoomEntity(RoomEntity roomEntity)
    {
        RoomEntities.Remove(roomEntity);
    }

    /*
     * Generates enemies in room
     * Enemy value is based of of previous rooms generated
     * Enemies in one room have between 1.5 to 2 times the value of enemies in the previous room populated, 
     * which should ensure there is always a way to defeat enemies in the room
     */
    public void PopulateRoom()
    {
        // Generate number of enemies
        int enemiesToSpawn = Random.Range(1, 3);

        // highest value in this room
        int highestLevelValue = 0;

        // Generate enemies
        for (int i = enemiesToSpawn - 1; i >= 0; i--)
        {
            // Spawn prefab and set up position
            Enemy newEnemy = Instantiate(EnemyPrefab, new Vector3(1.5f - i * 1.5f, 0, 0), Quaternion.identity);
            newEnemy.ParentRoom = this;
            newEnemy.transform.SetParent(this.transform, false);
            AddRoomEntity(newEnemy);

            // Calculate enemy value
            int value = (int)Mathf.Min(TotalEnemyValues, Mathf.Round(LastLevelMaxValue * 2f + Random.Range(-LastLevelMaxValue / 2f, 0)));
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
