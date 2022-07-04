using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum RoomEntityType
{
    Player,
    Enemy,
    Item
}

public class RoomEntity : MonoBehaviour
{
    public int Value;
    public RoomEntityType Type;
    public TMP_Text ValueText;
    public SpriteRenderer m_SpriteRenderer;
    public RoomEntityScriptableObject DataValues;

    // Start is called before the first frame update
    public virtual void Start()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        ValueText.text = Value.ToString();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }

    public void SetupDataValues(RoomEntityScriptableObject dataValues)
    {
        DataValues = dataValues;
        m_SpriteRenderer.sprite = dataValues.m_Sprite;
    }

    public virtual void CompareRoomEntity(RoomEntity other)
    {
        if (other.Value > this.Value)
        {
            other.ModifyValue(this.Value);
            this.DestroyEntity();
        }
        else
        {
            this.ModifyValue(other.Value);
            other.DestroyEntity();
        }
    }

    public virtual void SetValue(int value)
    {
        this.Value = value; 
        if (Value <= 0)
        {
            DestroyEntity();
        }
    }

    public virtual void ModifyValue(int modifyingValue)
    {
        Value += modifyingValue;

        ValueText.text = Value.ToString();

        if (Value <= 0)
        {
            DestroyEntity();
        }
    }

    public virtual void DestroyEntity()
    {
        Destroy(gameObject);
    }

    public virtual void OnMouseUp()
    {
        
    }
}
