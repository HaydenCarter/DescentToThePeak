using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;
public class Silhouette : MonoBehaviour
{
    public float lerpDuration = 3f;
    public float endAlpha = 0.5f;
    private Tilemap tilemap;
    public Color changedColor;

void Start()
    {
        tilemap = GetComponent<Tilemap>();
        foreach (Vector3Int tilePosition in tilemap.cellBounds.allPositionsWithin)
        {
            tilemap.RemoveTileFlags(tilePosition, TileFlags.LockColor);
        }
    }

    void ColorChange()
    {
        foreach (Vector3Int tilePosition in tilemap.cellBounds.allPositionsWithin)
        {
            tilemap.SetColor(tilePosition, changedColor);
        }
}
}