using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Silhouette2 : MonoBehaviour
{
    private Tilemap tilemap;
    public Color ColorToLerp;
    public float lerpDuration = 3f;

    private void Awake()
    {
        tilemap = GetComponent<Tilemap>();
    }

    void Start()
    {      
        foreach (Vector3Int tilePosition in tilemap.cellBounds.allPositionsWithin)
        {
            tilemap.RemoveTileFlags(tilePosition, TileFlags.LockColor);
        }
    }
    public void ColorChange()
    {
        foreach (Vector3Int tilePosition in tilemap.cellBounds.allPositionsWithin)
        {
            StartCoroutine(LerpColour(tilePosition));
            Debug.Log("Change Color!");
        }
    }
    public void SetBlack()
    {
        foreach (Vector3Int tilePosition in tilemap.cellBounds.allPositionsWithin)
        {
            var col = tilemap.GetColor(tilePosition);
            col = (Color.Lerp(col, new Color(0,0,0,1), 0.1f));
            tilemap.SetColor(tilePosition, col);
        }

    }
    public void SetNormal()
    {
        foreach (Vector3Int tilePosition in tilemap.cellBounds.allPositionsWithin)
        {
            tilemap.SetColor(tilePosition, new Color(1, 1, 1));
        }
    }
    IEnumerator LerpColour(Vector3Int tilePosition)
    {
        Color lerpedColour = tilemap.GetColor(tilePosition);
        float timeElapsed = 0;

        while (timeElapsed < lerpDuration)
        {
            lerpedColour = (Color.Lerp(lerpedColour, ColorToLerp, timeElapsed / lerpDuration));
            timeElapsed += Time.deltaTime;
            tilemap.SetColor(tilePosition, lerpedColour);
            yield return null;
        }
        tilemap.SetColor(tilePosition, lerpedColour);
    }
}
