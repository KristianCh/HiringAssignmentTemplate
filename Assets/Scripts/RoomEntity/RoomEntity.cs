using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoomEntity : MonoBehaviour
{
    // Value of entity
    public int Value;
    // Marks entity as dead
    public bool IsDead = false;
    // Value text component
    public TMP_Text ValueText;
    // Sprite
    public SpriteRenderer m_SpriteRenderer;
    // Scriptable object with data
    public RoomEntityScriptableObject DataValues;
    // Room in which this entity is
    public Room ParentRoom;
    // Rigidbody
    public Rigidbody2D m_Rigidbody2D;

    // Values
    protected float DeathForceMult = 500;
    protected float OutOfVisionHeight = -10;

    // Start is called before the first frame update
    public virtual void Start()
    {
        // Get sprite renderer
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        // Set value text
        ValueText.text = Value.ToString();
        ValueText.color *= new Color(Random.Range(0.5f, 1.5f), Random.Range(0.5f, 1.5f), Random.Range(0.5f, 1.5f), 1);
        ValueText.transform.rotation = Quaternion.Euler(0, 0, Random.Range(-10, 10));
    }

    // Update is called once per frame
    public virtual void Update()
    {
        // Destroy game object if can be destroyed
        if (CanBeDestroyed())
        {
            Destroy(gameObject);
        }
    }

    // Set up sprite from scriptable object
    public void SetupDataValues(RoomEntityScriptableObject dataValues)
    {
        DataValues = dataValues;
        m_SpriteRenderer.sprite = dataValues.m_Sprite;
    }

    /*
     * Compares two room entities, adds value to the larger one
     * If equal, prefers room entity from parameter
     * Parameter room entity should be player
     * 
     * Returns whether room entity from parameter was larger/equal
     */
    public virtual bool CompareRoomEntity(RoomEntity player)
    {
        SoundManager.Instance.PlayFightAudio();
        if (player.Value >= this.Value)
        {
            player.ModifyValue(this.Value);
            this.DestroyEntity();
            return true;
        }
        else
        {
            this.ModifyValue(player.Value);
            player.DestroyEntity();
            return false;
        }
    }

    // Set value of room entity, destroy if value is 0
    public virtual void SetValue(int value)
    {
        this.Value = value;

        ValueText.text = Value.ToString();

        if (Value <= 0)
        {
            DestroyEntity();
        }
    }

    // Add parameter to value
    public virtual void ModifyValue(int modifyingValue)
    {
        SetValue(Value + modifyingValue);
    }

    /* 
     * Mark entity as dead
     * GameObject is destroyed on next update
     * Any on death stuff should be done here
     */
    public virtual void DestroyEntity()
    {
        IsDead = true;
        ValueText.enabled = false;
        if (ParentRoom != null)
        {
            ParentRoom.RemoveRoomEntity(this);
        }
        transform.SetParent(null);
    }

    // If room entity can be destroyed
    public virtual bool CanBeDestroyed()
    {
        return IsDead && transform.position.y < OutOfVisionHeight; ;
    }

    // Enable dynamic rigidbody and apply force to it
    protected void ApplyDeathForce(float dirScale = 1f)
    {
        m_Rigidbody2D.isKinematic = false;
        float angle = Random.Range(0.5f, 0.9f);
        Vector3 force = new Vector3(Mathf.Cos(angle) * dirScale, Mathf.Sin(angle), 0) * DeathForceMult;
        m_Rigidbody2D.AddForceAtPosition(new Vector2(force.x, force.y), new Vector2(transform.position.x + Random.Range(-1f, 1f), transform.position.y + Random.Range(-1f, 1f)));
    }
}
