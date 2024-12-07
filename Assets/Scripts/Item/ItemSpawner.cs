using UnityEngine;
using UnityEngine.Tilemaps;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private Grid itemGrid;
    [SerializeField] private ItemMapping itemMapping;

    private Tilemap _itemTilemap;
    private GridInformation _itemGridInfo;

    public void Spawn(Vector3Int tilePos)
    {
        var tile = _itemTilemap.GetTile(tilePos);
        var tileObject = GetMatch(tile);
        if (tileObject == null) return;

        var offset = Quaternion.Euler(90f, 0f, 0f) * _itemTilemap.cellSize / 2;
        var objectPos = _itemTilemap.CellToWorld(tilePos) + offset;

        var itemObject = Instantiate(tileObject, objectPos, Quaternion.identity);
        _itemGridInfo.SetPositionProperty(tilePos, "itemObject", (Object)itemObject);
    }

    private GameObject GetMatch(TileBase tile)
    {
        return itemMapping.MarkerToPrefab(tile);
    }

    private void InitialSpawn()
    {
        var markerPosEnumerator = _itemTilemap.cellBounds.allPositionsWithin;

        foreach (var position in markerPosEnumerator)
            Spawn(position);
    }

    private void Start()
    {
        _itemTilemap = itemGrid.GetComponentInChildren<Tilemap>();
        _itemGridInfo = itemGrid.GetComponent<GridInformation>();

        InitialSpawn();
        _itemTilemap.GetComponent<TilemapRenderer>().enabled = false;
    }
}
