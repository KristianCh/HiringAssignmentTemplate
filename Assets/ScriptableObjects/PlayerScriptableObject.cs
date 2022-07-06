using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "Player")]
public class PlayerScriptableObject : ScriptableObject
{
    public float JumpSpeed;
    public float FloorOffset;
}
