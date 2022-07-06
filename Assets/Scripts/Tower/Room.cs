using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    // Position to move to
    public Vector3 TargetPosition = new Vector3(0, 2.4f, 0);
    // Offset to keep enemies feet on the floor
    private float FloorOffset = -0.3f;
    // List of room entities in room
    public List<RoomEntity> RoomEntities = new List<RoomEntity>();
    // Tower containing this room
    public Tower ParentTower;
    /*
     * Remove room when it is empty
     * Should be true for enemy tower and false for player tower
     */
    public bool RemoveOnEmpty = true;

    // Enemy prefab
    public Enemy EnemyPrefab;

    // Values of previous enemy and all enemies together
    private static int LastRoomMaxValue = 8;
    private static int TotalEnemyValues = 8;

    private float EnemySpawnOffset = 1.5f;
    // Multiplier of value from previously generated room
    private float ValueRoomMult = 2f;

    // Update is called once per frame
    public void Update()
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, TargetPosition, Time.deltaTime * 10);
    }

    // Reset static values - new game
    public static void ResetRoomValues()
    {
        LastRoomMaxValue = 8;
        TotalEnemyValues = 8;
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
            Player.Instance.BeginMovement(RoomEntities[0], -1);
            Player.onFinishMovementCallback += OnAttackMovementComplete;
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
        Player.onFinishMovementCallback = null;
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
                Player.onFinishMovementCallback += OnReturnToRoom;
            }
        }
        else
        {
            // Begin attack on next entity
            Player.Instance.BeginMovement(RoomEntities[0], -1);
            Player.onFinishMovementCallback += OnAttackMovementComplete;
        }
    }

    // When player returns to starting room, remove empty room
    public void OnReturnToRoom()
    {
        Player.onFinishMovementCallback = null;
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
        int highestRoomValue = 0;

        // Generate enemies
        for (int i = enemiesToSpawn - 1; i >= 0; i--)
        {
            // Spawn prefab and set up position
            Enemy newEnemy = Instantiate(EnemyPrefab, new Vector3(EnemySpawnOffset - i * EnemySpawnOffset, FloorOffset, 1), Quaternion.identity);
            newEnemy.ParentRoom = this;
            newEnemy.transform.SetParent(this.transform, false);
            AddRoomEntity(newEnemy);

            // Calculate enemy value
            int value = (int)Mathf.Min(
                    TotalEnemyValues, 
                    Mathf.Round(LastRoomMaxValue * ValueRoomMult + Random.Range(-LastRoomMaxValue / ValueRoomMult, 0))
                );
            // Set values
            newEnemy.SetValue(value);
            TotalEnemyValues += value;
            if (value > highestRoomValue) 
            { 
                highestRoomValue = value; 
            }
        }
        // Save max value from this room
        LastRoomMaxValue = highestRoomValue;
    }
}
