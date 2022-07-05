using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : RoomEntity
{
    public GameObject PickupParticlesPrefab;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        Type = RoomEntityType.Item;
    }

    public override void Update()
    {
        base.Update();
        // Destroy game object if marked as dead
        if (IsDead)
        {
            Destroy(gameObject);
        }
    }

    // Just add value to other and destroy item
    public override bool CompareRoomEntity(RoomEntity other)
    {
        other.ModifyValue(this.Value);
        this.DestroyEntity();
        return true;
    }

    public override void DestroyEntity()
    {
        base.DestroyEntity();
        Instantiate(PickupParticlesPrefab, transform.position, Quaternion.identity);   
    }
    
}
