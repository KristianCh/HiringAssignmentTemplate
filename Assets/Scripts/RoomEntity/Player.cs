using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : RoomEntity
{
    public static Player Instance;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        if (Instance != null)
        {
            Destroy(Instance.gameObject);
        }
        Instance = this;

        Type = RoomEntityType.Player;
    }

    public override void DestroyEntity()
    {
        Instance = null;
        base.DestroyEntity();
    }

    public void MoveToRoom(Room room)
    {
        transform.position = room.transform.position + new Vector3(0, -0.4f, 0);
    }
}
