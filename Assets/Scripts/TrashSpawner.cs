using UnityEngine;
using UnityEngine.Tilemaps;

public class TrashSpawner : MonoBehaviour
{
    private GameObject[] _trashes;
    [SerializeField] private Grid trashGrid;
    [SerializeField]
    public TrashMapping trashMapping
    {
        get; private set;
    }

    private Tilemap _trashTilemap;
    private GridInformation _gridInformation;

    /// <summary>
    /// Generates Trash at the given tilemap based position.
    /// </summary>
    /// <param name="tilePos">Coordinate on the tilemap</param>
    public void Spawn(Vector3Int tilePos)
    {
        var tile = _trashTilemap.GetTile(tilePos);
        var tileObject = GetMatch(tile);
        if (tileObject == null) return;

        var offset = Quaternion.Euler(90f, 0f, 0f) * _trashTilemap.cellSize / 2;
        var objectPos = _trashTilemap.CellToWorld(tilePos) + offset;

        var trashObject = Instantiate(tileObject, objectPos, Quaternion.identity);
        _gridInformation.SetPositionProperty(tilePos, "objectInstance", (Object)trashObject);
    }

    /// <summary>
    /// Generates trash at start. Please do not call if it's not the start of level.
    /// </summary>
    public void InitialSpawn()
    {
        var markerPosEnumerator = _trashTilemap.cellBounds.allPositionsWithin;

        foreach (var marker in markerPosEnumerator)
            Spawn(marker);
    }

    private GameObject GetMatch(TileBase marker)
    {
        return (marker == null) ? null : trashMapping.MarkerToPrefab(marker);
    }

    private void Start()
    {
        _trashTilemap = trashGrid.GetComponentInChildren<Tilemap>();
        _gridInformation = trashGrid.GetComponent<GridInformation>();
        trashMapping = Resources.Load("Storages/TrashMapping") as TrashMapping;

        InitialSpawn();
        _trashTilemap.GetComponent<TilemapRenderer>().enabled = false;
    }
}
