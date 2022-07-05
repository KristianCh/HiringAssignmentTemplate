using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
