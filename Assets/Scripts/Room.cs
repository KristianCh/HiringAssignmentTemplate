using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public List<RoomEntity> roomEntities = new List<RoomEntity>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FightRoomEntities()
    {
        for (int i = roomEntities.Count - 1; i >= 0; i--)
        {
            if (Player.Instance == null)
            {
                break;
            }
            roomEntities[i].CompareRoomEntity(Player.Instance);

            if (roomEntities[i] == null)
            {
                roomEntities.RemoveAt(i);
            }
        }
    }

    public void OnMouseUp()
    {
        FightRoomEntities();
    }
}
