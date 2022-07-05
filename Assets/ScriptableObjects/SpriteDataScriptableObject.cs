using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpriteData", menuName = "SpriteData")]
public class SpriteDataScriptableObject : ScriptableObject
{
    public List<Sprite> EnemySprites;
    public List<Sprite> PlayerSprites;
    public List<Sprite> ItemSprites;
    public List<Sprite> RoomSprites;
}