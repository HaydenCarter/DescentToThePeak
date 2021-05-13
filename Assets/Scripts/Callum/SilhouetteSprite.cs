using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SilhouetteSprite : MonoBehaviour
{
    public Color ColorToSet;
    public Color spriteColor;
    public float lerpDuration = 3f;
    public float endAlpha = 1f;
    // Start is called before the first frame update
    void Start()
    {
        spriteColor = GetComponent<SpriteRenderer>().color;
    }

    public void ColourChange()
    {
        StartCoroutine(LerpColour());
    }
    public void ReturnToNormal()
    {
        Color ColorToLerp = new Color(1f, 1f, 1f, 1f);
        ColourChange();
    }
    IEnumerator LerpColour()
    {
        Color spriteColor = GetComponent<SpriteRenderer>().color;
        float timeElapsed = 0;

        while (timeElapsed < lerpDuration)
        {
            spriteColor = Color.Lerp(spriteColor, ColorToSet, timeElapsed/lerpDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        spriteColor = new Color(spriteColor.r, spriteColor.g, spriteColor.b, endAlpha);
    }
} 
