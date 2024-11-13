using System;
using UnityEngine;
using UnityEngine.Tilemaps;

[Serializable]
public class ObstacleMapping
{
    public TileBase markerTile;
    public GameObject obstaclePrefab;

    public bool Match(TileBase other)
    {
        return (markerTile as Tile).sprite == (other as Tile).sprite;
    }
}