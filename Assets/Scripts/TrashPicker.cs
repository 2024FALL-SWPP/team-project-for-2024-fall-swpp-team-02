using UnityEngine;
using UnityEngine.Tilemaps;

public class TrashPicker : MonoBehaviour
{
    [SerializeField] private Grid trashGrid;

    private Tilemap _trashTilemap;
    private GridInformation _gridInfo;
    private PlayerBehaviour _playerBehaviour;

    private void Start()
    {
        _trashTilemap = trashGrid.GetComponentInChildren<Tilemap>();
        _gridInfo = trashGrid.GetComponent<GridInformation>();
        _playerBehaviour = FindObjectOfType<PlayerBehaviour>();
    }

    private void Update()
    {
        if (CheckIfBagIsFull()) return;
        if (!CheckIfTrashExists()) return;

        var trashObject = FindTrashAtCurrentPos();
        if (trashObject == null) return;

        var trashInfo = trashObject.GetComponent<TrashInfo>();

        _playerBehaviour.TriggerPickUpAnimation();
        AudioManager.Instance.PlaySFX("TrashPick");

        StageManager.Instance.bagController.AddTrash(trashInfo.trashType);

        var trashPosOnTilemap = _trashTilemap.WorldToCell(trashObject.transform.position);
        Destroy(trashObject, 0.15f);
        _trashTilemap.SetTile(trashPosOnTilemap, null);
    }

    private bool CheckIfBagIsFull()
    {
        return StageManager.Instance.bagController.IsBagFull();
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