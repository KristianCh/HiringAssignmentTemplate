using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class controlling movement between two point
public class MovementAnimator
{
    // Animation parameters
    public Player m_Player;
    public float Speed;
    public float T;
    public Vector3 StartPosition;
    public Vector3 EndPosition;

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
    public Vector3 UpdateAnimationPosition(float deltaTime)
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

    // Animator instance
    private MovementAnimator m_MovementAnimator;

    // Offset to keep players feet on the floor
    private float FloorOffset = -0.4f;

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
    }

    public override void Update()
    {
        base.Update();
        // If animator exists, update movement
        if (m_MovementAnimator != null)
        {
            Vector3 pos = m_MovementAnimator.UpdateAnimationPosition(Time.deltaTime);
            if (pos.z == -1)
            {
                // Movement finished, enable raycast collider, remove animator and invoke on finish movement functions
                m_Collider2D.enabled = true;
                m_MovementAnimator = null;
                onFinishMovement?.Invoke();

                // Set z to 0
                pos.z = 0;
            }
            // Set position
            transform.position = pos;
        }
    }

    // Begins movement to position of target
    public void BeginMovement(MonoBehaviour target, float xOffset = 0)
    {
        m_Collider2D.enabled = false;
        m_MovementAnimator = new MovementAnimator(this, transform.position, target.transform.position + new Vector3(xOffset, FloorOffset, 0));
    } 

    // Move player to room
    public void MoveToRoom(Room room)
    {
        ParentRoom = room;
        BeginMovement(room);
    }

    /*
     * Clear instance when destroying
     * Enable rigidbody and apply force
     */
    public override void DestroyEntity()
    {
        Instance = null;
        m_Collider2D.enabled = false;
        base.DestroyEntity();
        ApplyDeathForce(-1f);
    }
}
