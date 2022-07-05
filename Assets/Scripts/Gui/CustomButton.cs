using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomButton : MonoBehaviour
{
    public Image m_Image;
    // Start is called before the first frame update
    public void Start()
    {
        // Set appropriate sprite
        m_Image = GetComponent<Image>();
        if (AssetManager.Instance.SpriteData != null)
        {
            m_Image.sprite = AssetManager.Instance.SpriteData.ButtonSprite;
        }
    }

    // Play audio
    public void OnClickBase()
    {
        SoundManager.Instance.PlayButtonAudio();
    }
}
