using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTower : Tower
{
    // Item prefab
    public Item ItemPrefab;
    // Instance of player tower
    public static PlayerTower Instance;
    // Position to move to
    public Vector3 TargetPosition = new Vector3(-4, 0, 0);

    private int ItemSpawnDivisorLower = 10;
    private int ItemSpawnDivisorHigher = 5;


    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        // Set up instance
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
        }
        Instance = this;

        RoomList[0].TargetPosition = Vector3.zero;
    }

    public override void Update()
    {
        base.Update();  
        // Move to target position
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, TargetPosition, Time.deltaTime * 10);
    }

    // Do nothing
    public override void RemoveRoom(Room room)
    {
        return;
    }
    
    // Creates a room on top with a chance to generate an item
    public void AddRoomOnTop() 
    {
        if (RoomList.Count >= GameManager.Instance.MaxTowerHeight) return;
        Room newRoom = Instantiate(RoomPrefab, new Vector3(0, RoomHeight, 0), Quaternion.identity);
        newRoom.transform.SetParent(RoomList[RoomList.Count - 1].transform, false);
        newRoom.ParentTower = this;
        newRoom.RemoveOnEmpty = false;
        RoomList.Add(newRoom);

        GenerateItem(0.2f);
    }

    // Generates an item in the room with probability p (between 0 - 1)
    public void GenerateItem(float p)
    {
        if (Random.Range(0.0f, 1.0f) < p)
        {
            Item newItem = Instantiate(ItemPrefab, new Vector3(0, 0, 0.5f), Quaternion.identity);
            RoomList[RoomList.Count - 1].AddRoomEntity(newItem);

            newItem.SetValue(Random.Range(Player.Instance.Value / ItemSpawnDivisorLower, Player.Instance.Value / ItemSpawnDivisorHigher));
        }
    }
}
