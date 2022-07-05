using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName = "SpriteData", menuName = "SpriteData")]
public class SpriteDataScriptableObject : ScriptableObject
{
    public List<Sprite> EnemySprites;
    public List<Sprite> PlayerSprites;
    public List<Sprite> ItemSprites;
    public List<Sprite> RoomSprites;

    public Sprite ButtonSprite;
    public Sprite FloorSprite;
    public TMP_FontAsset Font;
}