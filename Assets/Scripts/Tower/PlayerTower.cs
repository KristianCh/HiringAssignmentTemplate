using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTower : Tower
{
    public Item ItemPrefab;
    public static PlayerTower Instance;

    // Start is called before the first frame update
    public override void Start()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
        }
        Instance = this;

        if (Random.Range(0f, 1f) > 0.5f)
        {
            Item newItem = Instantiate(ItemPrefab, Vector3.zero, Quaternion.identity);
            RoomList[1].AddRoomEntity(newItem);

            newItem.SetValue(Random.Range(3, 6));
        }
    }

    public override void RemoveRoom(int level)
    {
        return;
    }

    public void AddRoomOnTop() 
    {
        Room newRoom = Instantiate(RoomPrefab, new Vector3(0, RoomList.Count * 2.4f, 0), Quaternion.identity);
        newRoom.transform.SetParent(this.transform, false);
        newRoom.ParentTower = this;
        newRoom.RemoveOnEmpty = false;
        RoomList.Add(newRoom);
    }
}
