using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SilhouetteSprite : MonoBehaviour
{
    public Color ColorB;
    public float speed = 3f;
    public SpriteRenderer spriteRenderer;
    public Color ColorA;

    // Start is called before the first frame update
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        ColorA = spriteRenderer.color;
    }

    public void ColourChange()
    {
        spriteRenderer.color = Color.Lerp(ColorA, ColorB, speed);
    }
    public void ReturnToNormal()
    {
        Color ColorB = new Color(1f, 1f, 1f, 1f);
        ColourChange();
    }
} 
