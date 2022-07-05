using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    // Number of rooms in tower
    public int Levels = 4;
    // List of rooms in tower
    public List<Room> RoomList = new List<Room>();

    // Room prefab
    public Room RoomPrefab;

    // Height og 1 room
    protected float RoomHeight = 2.4f;

    // Start is called before the first frame update
    public virtual void Start()
    {

    }

    public virtual void Update()
    {

    }

    /*
     * Remove room at level from tower, move rooms above removed down
     * Rooms should only be removed from enemy towers
     */
    public virtual void RemoveRoom(Room room)
    {
        // Add a room to the top of player tower
        PlayerTower.Instance.AddRoomOnTop();

        // Offset to move rooms by
        Vector3 offset = new Vector3(0, RoomHeight, 0);

        // Move rooms above down
        int index = RoomList.IndexOf(room);
        if (index < RoomList.Count - 1)
        {
            if (index > 0) RoomList[index + 1].transform.position += offset;
            RoomList[index + 1].transform.SetParent(room.transform.parent, false);
        }
        // Remove given room
        RoomList.Remove(room);

        // If no rooms left destroy tower
        if (RoomList.Count == 0)
        {
            DestroyTower();
        }
        else
        {
            RoomList[0].TargetPosition = Vector3.zero;
        }
    }

    /*
     * Builds new tower
     * Rooms are built in a shuffled order, because room population creates enemies stronger than previous rooms.
     * When rooms are in a shuffled order, player has to actually check rooms, not just go from the bottom
     */
    public void GenerateTower()
    {
        // Generate indices array from 0 to Levels and initialize room list
        int[] indices = new int[Levels];
        for (int i = 0; i < Levels; i++)
        {
            RoomList.Add(null);
            indices[i] = i;
        }
            
        // Shuffle indices array
        for (int i = 0; i < Levels; i++)
        {
            int rnd = Random.Range(0, Levels);
            int temp = indices[rnd];
            indices[rnd] = indices[i];
            indices[i] = temp;
        }

        // Instantiate new rooms
        for (int j = 0; j < Levels; j++)
        {
            int i = indices[j];
            Room newRoom = Instantiate(RoomPrefab, new Vector3(0, RoomHeight, 0.5f), Quaternion.identity);
            newRoom.transform.SetParent(this.transform, false);
            newRoom.ParentTower = this;
            RoomList[i] = newRoom;

            // Populate room with enemies
            newRoom.PopulateRoom();
        }

        //
        RoomList[0].TargetPosition = Vector3.zero;
        RoomList[0].transform.localPosition = Vector3.zero;
        // Set parent rooms for each above first
        for (int i = 1; i < Levels; i++)
        {
            RoomList[i].transform.SetParent(RoomList[i-1].transform, false);
        }
    }

    // Move player tower to this position and destroy game object
    public void DestroyTower()
    {
        LevelManager.Instance.OnTowerDestroyed(this);
        Destroy(gameObject);
    }
}
