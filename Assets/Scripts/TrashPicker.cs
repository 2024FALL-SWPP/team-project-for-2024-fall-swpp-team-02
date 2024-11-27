using UnityEngine;
using UnityEngine.Tilemaps;

public class TrashPicker : MonoBehaviour
{
    [SerializeField] private Grid trashGrid;

    private Tilemap _trashTilemap;
    private GridInformation _gridInfo;

    private InventoryUI _inventoryUI;

    private void Start()
    {
        _trashTilemap = trashGrid.GetComponentInChildren<Tilemap>();
        _gridInfo = trashGrid.GetComponent<GridInformation>();

        _inventoryUI = FindObjectOfType<InventoryUI>();
    }

    private void Update()
    {
        if (!CheckIfTrashExists()) return;

        var trashObject = FindTrashAtCurrentPos();
        if (trashObject == null) return;

        var trashInfo = trashObject.GetComponent<TrashInfo>();

        StageManager.Instance.bagController.AddTrash(trashInfo.trashType);

        var trashPosOnTilemap = _trashTilemap.WorldToCell(trashObject.transform.position);
        Destroy(trashObject);
        _trashTilemap.SetTile(trashPosOnTilemap, null);
        _inventoryUI.UpdateInventory(StageManager.Instance.bagController.GetTrashList());

        if (ScoreModel.Instance != null)
        {
            ScoreModel.Instance.IncTrashPickupCount();
        }

    }

    private GameObject FindTrashAtCurrentPos()
    {
        var playerPosOnTilemap = _trashTilemap.WorldToCell(transform.position);

        return _gridInfo.GetPositionProperty<GameObject>(playerPosOnTilemap, "objectInstance", null);
    }

    private bool CheckIfTrashExists()
    {
        return _trashTilemap.HasTile(_trashTilemap.WorldToCell(transform.position));
    }
}