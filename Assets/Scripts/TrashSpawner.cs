using UnityEngine;
using UnityEngine.Tilemaps;

public class TrashSpawner : MonoBehaviour
{
    private GameObject[] _trashes;
    [SerializeField] private Tilemap trashTilemap;
    [SerializeField] private TrashMapping[] trashMappings;
    [SerializeField] private TrashStorage trashStorage;
    
    /// <summary>
    /// Generates Trash at the given tilemap based position.
    /// </summary>
    /// <param name="tilePos">Coordinate on the tilemap</param>
    public void Spawn(Vector3Int tilePos)
    {
        var tile = trashTilemap.GetTile(tilePos);
        var tileObject = GetMatch(tile);
        if (tileObject == null) return;
        
        var offset = Quaternion.Euler(90f, 0f, 0f) * trashTilemap.cellSize / 2;
        var objectPos = trashTilemap.CellToWorld(tilePos) + offset;
        
        var trashObject = Instantiate(tileObject, objectPos, Quaternion.identity);
        trashStorage.AddTrash(trashObject);
    }

    /// <summary>
    /// Generates trash at start. Please do not call if it's not the start of level.
    /// </summary>
    public void InitialSpawn()
    {
        var markerPosEnumerator = trashTilemap.cellBounds.allPositionsWithin;
        
        foreach (var marker in markerPosEnumerator)
            Spawn(marker);
    }

    private GameObject GetMatch(TileBase marker)
    {
        if (marker == null) return null;
        foreach (var mapping in trashMappings)
            if (mapping.Match(marker)) return mapping.GetRandomPrefab();

        return null;
    }

    private void Start()
    {
        trashStorage.Prune();
        InitialSpawn();
        trashTilemap.GetComponent<TilemapRenderer>().enabled = false;
    }
}
