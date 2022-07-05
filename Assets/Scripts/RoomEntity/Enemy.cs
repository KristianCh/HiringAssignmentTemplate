using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : RoomEntity
{
    public override void Start()
    {
        base.Start();
        if (SpriteData != null && SpriteData.EnemySprites.Count > 0)
        {
            m_SpriteRenderer.sprite = SpriteData.EnemySprites[Random.Range(0, SpriteData.EnemySprites.Count)];
        }
    }

    // Call base destroy and apply force
    public override void DestroyEntity()
    {
        base.DestroyEntity();
        ApplyDeathForce();
    }
}
