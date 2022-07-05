using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : RoomEntity
{
    // Particles prefab
    public GameObject PickupParticlesPrefab;

    public override void Start()
    {
        base.Start();
        if (SpriteData != null && SpriteData.ItemSprites.Count > 0)
        {
            m_SpriteRenderer.sprite = SpriteData.ItemSprites[Random.Range(0, SpriteData.ItemSprites.Count)];
        }
    }

    // Add value to other and destroy item
    public override bool CompareRoomEntity(RoomEntity other)
    {
        SoundManager.Instance.PlayPickupAudio();
        other.ModifyValue(this.Value);
        this.DestroyEntity();
        return true;
    }

    // Create particles on destroy
    public override void DestroyEntity()
    {
        base.DestroyEntity();
        Instantiate(PickupParticlesPrefab, transform.position, Quaternion.identity);
    }

    // remove position requirement to be destroyed
    public override bool CanBeDestroyed()
    {
        return IsDead;
    }
}
