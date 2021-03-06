using System.Collections;
using UnityEngine;

public class Fade : MonoBehaviour
{

    public float changeTimeSeconds = 5;
    public float startAlpha = 0;
    public float endAlpha = 1;
    public float ChangeTime2;

    float changeRate = 0;
    float timeSoFar = 0;
    bool fading = false;
    CanvasGroup canvasGroup;


    void Awake()
    {
        canvasGroup = this.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            Debug.Log("Must have canvas group attached!");
            this.enabled = false;
        }
    }

    public void FadeIn()
    {
        startAlpha = 0;
        endAlpha = 1;
        timeSoFar = 0;
        fading = true;
        StartCoroutine(FadeCoroutine());
        Debug.Log("Fading In");
    }

    public void FadeOut()
    {
        startAlpha = 1;
        endAlpha = 0;
        timeSoFar = 0;
        fading = true;
        StartCoroutine(FadeCoroutine());
        Debug.Log("Fading Out");
    }

    IEnumerator FadeCoroutine()
    {
        changeRate = (endAlpha - startAlpha) / changeTimeSeconds;
        SetAlpha(startAlpha);
        while (fading)
        {
            timeSoFar += Time.deltaTime;

            if (timeSoFar > changeTimeSeconds)
            {
                fading = false;
                SetAlpha(endAlpha);
                yield break;
            }
            else
            {
                SetAlpha(canvasGroup.alpha + (changeRate * Time.deltaTime));
            }

            yield return null;
        }
    }

    public void SetSpeed()
    {
        changeTimeSeconds = ChangeTime2;
            }

    public void SetAlpha(float alpha)
    {
        canvasGroup.alpha = Mathf.Clamp(alpha, 0, 1);
    }
}