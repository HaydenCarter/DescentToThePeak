using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SpriteChanger : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite idleSprite;
    public Sprite moveSprite;
    public Sprite climbSprite;
    public Sprite sitSprite;

    public void IdleSprite()
    {
        spriteRenderer.sprite = idleSprite;
    }
    public void MoveSprite()
    {
        spriteRenderer.sprite = moveSprite;
    }
public void ClimbSprite()
{
    spriteRenderer.sprite = climbSprite;
    }
    public void SitSprite()
{
    spriteRenderer.sprite = sitSprite;
    }
}


