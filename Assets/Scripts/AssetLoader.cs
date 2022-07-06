using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AssetLoader : MonoBehaviour
{
    // Components to set
    public TMP_Text m_Text;
    public Image m_Image;
    public SpriteRenderer m_SpriteRenderer;
    // Selection of sprite
    public SpriteSelection Type;

    // Start is called before the first frame update
    public void Start()
    {
        // Apply sprites/font
        Sprite sprite = AssetManager.Instance.GetSpriteAsset(Type);
        TMP_FontAsset font = AssetManager.Instance.GetFontAsset();
        if (m_SpriteRenderer != null && sprite != null)
        {
            m_SpriteRenderer.sprite = sprite;
        }
        if (m_Image != null && sprite != null)
        {
            m_Image.sprite = sprite;
        }
        if (m_Text != null && font != null)
        {
            m_Text.font = font;
        }
    }
}
