using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CustomText : MonoBehaviour
{
    public TMP_Text m_Text;
    // Start is called before the first frame update
    void Start()
    {
        // Set appropriate font
        m_Text = GetComponent<TMP_Text>();
        if (AssetManager.Instance.SpriteData != null)
        {
            m_Text.font = AssetManager.Instance.SpriteData.Font;
        }
    }
}
