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
        for (int i = 0; i < Levels; i++)
        {
            Room newRoom = Instantiate(RoomPrefab, new Vector3(0, i * 2.4f, 0), Quaternion.identity);
            newRoom.Level = i + 1;
            newRoom.transform.SetParent(this.transform, false);
            newRoom.ParentTower = this;
            RoomList.Add(newRoom);

            newRoom.PopulateRoom(Difficulty);
        }
    }

    public void DestroyTower()
    {
        PlayerTower.Instance.transform.position = transform.position;
        Destroy(gameObject);
    }
}
