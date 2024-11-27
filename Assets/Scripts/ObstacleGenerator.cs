using UnityEngine;
using UnityEngine.Tilemaps;

public class ObstacleGenerator : MonoBehaviour
{
    [SerializeField] private Tilemap obstacleTilemap;
    [SerializeField] private ObstacleMapping[] obstacleMappings;

    private void Start()
    {
        GenerateObstacles();
    }

    /// <summary>
    /// Generates obstacles at the tile marker position.
    /// </summary>
    public void GenerateObstacles()
    {
        var markerPosEnumerator = obstacleTilemap.cellBounds.allPositionsWithin;

        foreach (var markerPos in markerPosEnumerator)
        {
            var tile = obstacleTilemap.GetTile(markerPos);
            var tileObject = GetMatch(tile);
            if (tileObject == null) continue;

            var offset = Quaternion.Euler(90f, 0f, 0f) * obstacleTilemap.cellSize / 2;
            var objectPos = obstacleTilemap.CellToWorld(markerPos) + offset;

            Instantiate(tileObject, objectPos, Quaternion.identity);
        }

        obstacleTilemap.GetComponent<TilemapRenderer>().enabled = false;
    }

    private GameObject GetMatch(TileBase marker)
    {
        if (marker == null) return null;

        foreach (var mapping in obstacleMappings)
            if (mapping.Match(marker)) return mapping.obstaclePrefab;

        return null;
    }
}