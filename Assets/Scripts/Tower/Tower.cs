using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public int Levels = 4;
    public int Difficulty = 1;
    public List<Room> RoomList = new List<Room>();

    public Room RoomPrefab;

    // Start is called before the first frame update
    public virtual void Start()
    {
        GenerateTower();
    }

    public virtual void RemoveRoom(int level)
    {
        PlayerTower.Instance.AddRoomOnTop();

        Vector3 offset = new Vector3(0, 2.4f, 0);
        RoomList.RemoveAt(level - 1);
        for (int i = level - 1; i < RoomList.Count; i++)
        {
            RoomList[i].transform.position -= offset;
            RoomList[i].Level--;
        }

        if (RoomList.Count == 0)
        {
            DestroyTower();
        }
    }

    public void GenerateTower()
    {
        // Generate array 0 to Levels
        int[] indices = new int[Levels];
        for (int i = 0; i < Levels; i++)
        {
            RoomList.Add(null);
            indices[i] = i;
        }
            
        // Shuffle array
        for (int i = 0; i < Levels; i++)
        {
            int rnd = Random.Range(0, Levels);
            int temp = indices[rnd];
            indices[rnd] = indices[i];
            indices[i] = temp;
        }

        for (int j = 0; j < Levels; j++)
        {
            int i = indices[j];
            Room newRoom = Instantiate(RoomPrefab, new Vector3(0, i * 2.4f, 0), Quaternion.identity);
            newRoom.Level = i + 1;
            newRoom.transform.SetParent(this.transform, false);
            newRoom.ParentTower = this;
            RoomList[i] = newRoom;

            newRoom.PopulateRoom(Difficulty);
        }
    }

    public void DestroyTower()
    {
        PlayerTower.Instance.transform.position = transform.position;
        Destroy(gameObject);
    }
}
