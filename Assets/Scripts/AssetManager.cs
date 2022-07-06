using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum SpriteSelection
{
    Enemy,
    Player,
    Item,
    Room,
    Button,
    Floor,
    None
}

public class AssetManager : MonoBehaviour
{
    public SpriteDataScriptableObject SpriteData;

    public static AssetManager Instance;

    // Start is called before the first frame update
    void Start()
    {
        // Set up instance
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    // Return sprite appropriate for sprite selection type
    public Sprite GetSpriteAsset(SpriteSelection spriteSelection)
    {
        Sprite sprite = null;
        switch (spriteSelection)
        {
            case SpriteSelection.Enemy:
                if (SpriteData.PlayerSprites.Count > 0)
                {
                    sprite = SpriteData.EnemySprites[Random.Range(0, SpriteData.EnemySprites.Count)];
                }
                break;
            case SpriteSelection.Player:
                if (SpriteData.PlayerSprites.Count > 0)
                {
                    sprite = SpriteData.PlayerSprites[Random.Range(0, SpriteData.PlayerSprites.Count)];
                }
                break;
            case SpriteSelection.Item:
                if (SpriteData.PlayerSprites.Count > 0)
                {
                    sprite = SpriteData.ItemSprites[Random.Range(0, SpriteData.ItemSprites.Count)];
                }
                break;
            case SpriteSelection.Room:
                if (SpriteData.PlayerSprites.Count > 0)
                {
                    sprite = SpriteData.RoomSprites[Random.Range(0, SpriteData.RoomSprites.Count)];
                }
                break;
            case SpriteSelection.Button:
                sprite = SpriteData.ButtonSprite;
                break;
            case SpriteSelection.Floor:
                sprite = SpriteData.FloorSprite;
                break;
            default:
                sprite = null;
                break;
        }
        return sprite;
    }

    // Return font asset
    public TMP_FontAsset GetFontAsset()
    {
        return SpriteData.Font;
    }
}
