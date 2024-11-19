using UnityEngine;
using UnityEngine.Tilemaps;

public class TrashPicker : MonoBehaviour
{
    private Vector3 _offset;
    
    [SerializeField] private Tilemap trashTilemap;
    [SerializeField] private TrashStorage trashStorage;
    private PlayerBagController _bagController;

    private void Start()
    {
        _offset = Quaternion.Euler(90f, 0f, 0f) * trashTilemap.cellSize / 2;
        _bagController = new PlayerBagController(6);
    }

    private void Update()
    {
        if (!CheckIfTrashExists()) return;

        var trashObject = FindTrashAtCurrentPos();
        if (trashObject == null) return;
        
        var trashInfo = trashObject.GetComponent<TrashInfo>();
        _bagController.AddTrash(trashInfo.trashType, trashInfo.trashSubtype);
        
        var trashPosOnTilemap = trashTilemap.WorldToCell(trashObject.transform.position);

        trashStorage.RemoveTrash(trashObject);
        trashTilemap.SetTile(trashPosOnTilemap, null);
    }

    private GameObject FindTrashAtCurrentPos()
    {
        var playerPosOnTilemap = trashTilemap.WorldToCell(transform.position);
        var searchingTrashPos = trashTilemap.CellToWorld(playerPosOnTilemap) + _offset;
        return trashStorage.SearchByPosition(searchingTrashPos);
    }

    private bool CheckIfTrashExists()
    {
        return trashTilemap.HasTile(trashTilemap.WorldToCell(transform.position));
    }
}