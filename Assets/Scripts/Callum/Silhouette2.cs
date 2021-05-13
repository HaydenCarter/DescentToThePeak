﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Silhouette2 : MonoBehaviour
{
    private Tilemap tilemap;
    public Color ColorToLerp;
    public float lerpDuration = 3f;
    void Start()
    {
        tilemap = GetComponent<Tilemap>();
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
    public void ReturnToNormal()
    {
        Color ColorToLerp = new Color(1f, 1f, 1f);
        ColorChange();
    }
    IEnumerator LerpColour(Vector3Int tilePosition)
    {
        Color lerpedColour = tilemap.GetColor(tilePosition);
        float timeElapsed = 0;

        while (timeElapsed < lerpDuration)
        {
            lerpedColour = (Color.Lerp(lerpedColour, ColorToLerp, timeElapsed/lerpDuration));
            tilemap.SetColor(tilePosition, lerpedColour);
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        lerpedColour = new Color(lerpedColour.r, lerpedColour.g, lerpedColour.b);
        tilemap.SetColor(tilePosition, lerpedColour);
        tilemap.SetTileFlags(tilePosition, TileFlags.LockColor);
    }
}