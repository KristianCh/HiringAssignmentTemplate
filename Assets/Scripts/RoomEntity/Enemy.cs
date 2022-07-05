using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : RoomEntity
{
    // Call base destroy and apply force
    public override void DestroyEntity()
    {
        base.DestroyEntity();
        ApplyDeathForce();
    }
}
