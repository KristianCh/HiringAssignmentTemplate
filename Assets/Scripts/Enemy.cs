using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : RoomEntity
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        Type = RoomEntityType.Enemy;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public override void OnMouseUp()
    {
        Debug.Log("MouseUp");
        CompareRoomEntity(Player.Instance);
    }
}
