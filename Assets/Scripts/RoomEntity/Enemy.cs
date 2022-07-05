using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : RoomEntity
{
    public override void Start()
    {
        base.Start();
        // Set random sprite from selection
        if (AssetManager.Instance.SpriteData != null && AssetManager.Instance.SpriteData.EnemySprites.Count > 0)
        {
            m_SpriteRenderer.sprite =
                AssetManager.Instance.SpriteData.EnemySprites[Random.Range(0, AssetManager.Instance.SpriteData.EnemySprites.Count)];
        }
    }

    // Call base destroy and apply force
    public override void DestroyEntity()
    {
        base.DestroyEntity();
        ApplyDeathForce();
    }
}
