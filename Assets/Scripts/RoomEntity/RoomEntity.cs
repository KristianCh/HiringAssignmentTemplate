using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Entity type
public enum RoomEntityType
{
    Player,
    Enemy,
    Item
}

public class RoomEntity : MonoBehaviour
{
    // Value of entity
    public int Value;
    // Marker to delete next update
    public bool IsDead = false;
    // Entity Type
    public RoomEntityType Type;
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
}
