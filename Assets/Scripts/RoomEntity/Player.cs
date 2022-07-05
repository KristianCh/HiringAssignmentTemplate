using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class controlling movement between two point
public class MovementAnimator
{
    public Player m_Player;
    public float Speed;
    public float T;

    public Vector3 StartPosition;
    public Vector3 EndPosition;
    private Vector3 Heading => (EndPosition - StartPosition).normalized;

    // Initialize values
    public MovementAnimator(Player player, Vector3 startPosition, Vector3 endPosition, float speed = 1)
    {
        m_Player = player;
        StartPosition = startPosition;
        EndPosition = endPosition;
        Speed = speed;
        T = 0;
    }

    // Interpolate between positions and add an arc
    // Returns position vector, z value is -1 if finished
    public Vector3 Update(float deltaTime)
    {
        T = Mathf.Min(1, T + Speed * deltaTime);

        if (T >= 1)
        {
            return new Vector3(EndPosition.x, EndPosition.y, -1);
        }
        return Vector3.Lerp(StartPosition, EndPosition, T) + new Vector3(0, Mathf.Sin(T * Mathf.PI) * 2, 0);
    }
}

public class Player : RoomEntity
{
    // Instance
    public static Player Instance;
    // Collider for drag line raycast
    public Collider2D m_Collider2D;

    // Invoked on finishing of movement
    public delegate void OnFinishMovement();
    public static OnFinishMovement onFinishMovement;

    private MovementAnimator m_MovementAnimator;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        // Set up player instance
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
        }
        Instance = this;

        Type = RoomEntityType.Player;
    }

    public override void Update()
    {
        base.Update();
        // Destroy game object if marked as dead and out of view
        if (IsDead && transform.position.y < -10)
        {
            Destroy(gameObject);
        }

        // If animator exists, update movement
        if (m_MovementAnimator != null)
        {
            Vector3 pos = m_MovementAnimator.Update(Time.deltaTime);
            if (pos.z == -1)
            {
                // Movement finished, enable raycast collider, remove animator and invoke on finish movement functions
                m_Collider2D.enabled = true;
                m_MovementAnimator = null;
                onFinishMovement?.Invoke();

                // Set position
                pos.z = 0;
                transform.position = pos;
            }
            else
            {
                transform.position = pos;
            }
        }
    }

    // Begins movement toward enemy
    public void BeginAttackMovement(RoomEntity roomEntity)
    {
        m_Collider2D.enabled = false;
        m_MovementAnimator = new MovementAnimator(this, transform.position, roomEntity.transform.position + new Vector3(-1, -0.4f, 0));
    }

    // Move player to room
    public void MoveToRoom(Room room)
    {
        m_Collider2D.enabled = false;
        ParentRoom = room;
        Vector3 targetPosition = room.transform.position;
        m_MovementAnimator = new MovementAnimator(this, transform.position, targetPosition + new Vector3(0, -0.4f, 0));
    }

    /*
     * Clear instance when destroying
     * Enable rigidbody and apply force
     */
    public override void DestroyEntity()
    {
        Instance = null;
        base.DestroyEntity();
        m_Rigidbody2D.isKinematic = false;
        m_Collider2D.enabled = false;
        float angle = Random.Range(0.5f, 0.9f);
        Vector3 force = new Vector3(-Mathf.Cos(angle), Mathf.Sin(angle), 0) * 500;
        m_Rigidbody2D.AddForceAtPosition(new Vector2(force.x, force.y), new Vector2(transform.position.x + Random.Range(-1f, 1f), transform.position.y + Random.Range(-1f, 1f)));
    }
}
