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

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public override void CompareRoomEntity(RoomEntity other)
    {
        other.ModifyValue(Value); 
        DestroyEntity();
    }
}
