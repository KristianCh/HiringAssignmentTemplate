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

    public override void Update()
    {
        base.Update();
        // Destroy game object if marked as dead and out of view
        if (IsDead && transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }

    // Enable rigidbody and apply force
    public override void DestroyEntity()
    {
        base.DestroyEntity();
        m_Rigidbody2D.isKinematic = false;
        float angle = Random.Range(0.5f, 0.9f);
        Vector3 force = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * 500;
        m_Rigidbody2D.AddForceAtPosition(new Vector2(force.x, force.y), new Vector2(transform.position.x + Random.Range(-1f, 1f), transform.position.y + Random.Range(-1f, 1f)));
    }
}
