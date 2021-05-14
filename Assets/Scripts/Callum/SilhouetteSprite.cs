using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SilhouetteSprite : MonoBehaviour
{
    public Color ColorB;
    public SpriteRenderer spriteToChange;
    public Color ColorA;
    public Color ColorC;
    public float lerpDuration = 3;

    // Start is called before the first frame update
    void Start()
    {
        spriteToChange = GetComponent<SpriteRenderer>();
    }

    public void ColourChange()
    {
        StartCoroutine(LerpFunction(ColorB, lerpDuration));
    }
    public void ColourChange2()    
    {
        StartCoroutine(LerpFunction(ColorC, lerpDuration));
    }

    IEnumerator LerpFunction(Color endValue, float duration)
    {
        float time = 0;

        Color ColorA = spriteToChange.color;

        while (time < duration)
        {
            spriteToChange.color = Color.Lerp(ColorA, endValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        spriteToChange.color = endValue;
    }
}