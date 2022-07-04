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
    public bool IsDead = false;
    public RoomEntityType Type;
    public TMP_Text ValueText;
    public SpriteRenderer m_SpriteRenderer;
    public RoomEntityScriptableObject DataValues;
    public Room ParentRoom;

    // Start is called before the first frame update
    public virtual void Start()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        ValueText.text = Value.ToString();
        ValueText.transform.rotation = Quaternion.Euler(0, 0, Random.Range(-10, 10));
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (IsDead)
        {
            Destroy(gameObject);
        }
    }

    public void SetupDataValues(RoomEntityScriptableObject dataValues)
    {
        DataValues = dataValues;
        m_SpriteRenderer.sprite = dataValues.m_Sprite;
    }

    public virtual bool CompareRoomEntity(RoomEntity other)
    {
        if (other.Value > this.Value)
        {
            other.ModifyValue(this.Value);
            this.DestroyEntity();
            return true;
        }
        else
        {
            this.ModifyValue(other.Value);
            other.DestroyEntity();
            return false;
        }
    }

    public virtual void SetValue(int value)
    {
        this.Value = value;

        ValueText.text = Value.ToString();

        if (Value <= 0)
        {
            DestroyEntity();
        }
    }

    public virtual void ModifyValue(int modifyingValue)
    {
        SetValue(Value + modifyingValue);
    }

    public virtual void DestroyEntity()
    {
        IsDead = true;
        if (ParentRoom != null)
        {
            ParentRoom.RemoveRoomEntity(this);
        }
    }
}
