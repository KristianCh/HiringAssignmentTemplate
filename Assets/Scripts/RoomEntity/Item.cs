using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : RoomEntity
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        Type = RoomEntityType.Item;
    }

    public override bool CompareRoomEntity(RoomEntity other)
    {
        other.ModifyValue(this.Value);
        this.DestroyEntity();
        return true;
    }
}
