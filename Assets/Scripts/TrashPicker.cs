using UnityEngine;
using UnityEngine.Tilemaps;

public class TrashPicker : MonoBehaviour
{
    [SerializeField] private Grid trashGrid;

    private Tilemap _trashTilemap;
    private GridInformation _gridInfo;
    private PlayerBehaviour _playerBehaviour;
    private Vector3Int lastVisitedPos = new(-100, -100, -100);

    private void Start()
    {
        _trashTilemap = trashGrid.GetComponentInChildren<Tilemap>();
        _gridInfo = trashGrid.GetComponent<GridInformation>();
        _playerBehaviour = FindObjectOfType<PlayerBehaviour>();
    }

    private void Update()
    {
        var markerPos = _trashTilemap.WorldToCell(transform.position);
        bool alert = markerPos != lastVisitedPos;
        lastVisitedPos = markerPos;

        if (!CheckIfTrashExists(markerPos)) return;
        if (CheckIfBagIsFull())
        {
            if (alert)
            {
                AudioManager.Instance.PlaySFX("MotionFail");
                GameObject.Find("InventoryBackground").GetComponent<UIShake>().Shake(0.25f, 5f);
            }
            return;
        }

        var trashObject = FindTrashAtPos(markerPos);
        if (trashObject == null) return;

        var trashInfo = trashObject.GetComponent<TrashBehaviour>();

        _playerBehaviour.TriggerPickUpAnimation();
        AudioManager.Instance.PlaySFX("TrashPick");

        StageManager.Instance.bagController.AddTrash(trashInfo.trashType);
        Destroy(trashObject, 0.15f);
        _trashTilemap.SetTile(markerPos, null);
    }

    private bool CheckIfBagIsFull()
    {
        return StageManager.Instance.bagController.IsBagFull();
    }

    private GameObject FindTrashAtPos(Vector3Int markerPos)
    {

        return _gridInfo.GetPositionProperty<GameObject>(markerPos, "objectInstance", null);
    }

    private bool CheckIfTrashExists(Vector3Int markerPos)
    {
        return _trashTilemap.HasTile(markerPos);
    }
}