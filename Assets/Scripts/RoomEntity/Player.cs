using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : RoomEntity
{
    // Instance
    public static Player Instance;
    // Collider for drag line raycast
    public Collider2D m_Collider2D;

    // Invoked on finishing of movement
    public delegate void OnFinishMovementCallback();
    public static OnFinishMovementCallback onFinishMovementCallback;
    public PlayerScriptableObject PlayerData;

    // Animator instance
    private MovementAnimator m_MovementAnimator;

    // Offset to keep players feet on the floor
    private float FloorOffset = -0.4f;
    private float JumpSpeed = 1f;

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

        // Set random sprite from selection
        if (AssetManager.Instance.SpriteData != null && AssetManager.Instance.SpriteData.PlayerSprites.Count > 0)
        {
            m_SpriteRenderer.sprite =
                AssetManager.Instance.SpriteData.PlayerSprites[Random.Range(0, AssetManager.Instance.SpriteData.PlayerSprites.Count)];
        }
        // Set up values from scriptable object
        if (PlayerData != null)
        {
            JumpSpeed = PlayerData.JumpSpeed;
            FloorOffset = PlayerData.FloorOffset;
            if (PlayerData.OverrideSprite)
            {
                m_SpriteRenderer.sprite = PlayerData.PlayerSprite;
            }
        }
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
                onFinishMovementCallback?.Invoke();

                // Set z to 0
                pos.z = 0;
            }
            // Set position
            transform.position = pos;
        }
        // Make camera follow player
    }

    // Begins movement to position of target
    public void BeginMovement(MonoBehaviour target, float xOffset = 0)
    {
        SoundManager.Instance.PlayJumpAudio();
        m_Collider2D.enabled = false;
        m_MovementAnimator = new MovementAnimator(this, transform.position, target.transform.position + new Vector3(xOffset, FloorOffset, 0.5f), JumpSpeed);
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
        LevelManager.Instance.OnPlayerDeath();
    }
}
