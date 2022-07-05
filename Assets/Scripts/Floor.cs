using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    public SpriteRenderer m_SpriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        // Set appropriate sprite
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        if (AssetManager.Instance.SpriteData != null)
        {
            m_SpriteRenderer.sprite = AssetManager.Instance.SpriteData.FloorSprite;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Keep y position at floor level
        Vector3 newPos = transform.position;
        newPos.y = -7.7f;
        transform.position = newPos; 
    }
}
