using System.Collections;
using System.Numerics;
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
        var poppedTrash = StageManager.Instance.bagController.AddTrash(trashInfo.trashType);
        var trashPosOnTilemap = _trashTilemap.WorldToCell(trashObject.transform.position);
        _trashTilemap.SetTile(trashPosOnTilemap, null);
        StartCoroutine(ReplaceTrash(trashPosOnTilemap, trashObject, poppedTrash));
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

    private IEnumerator ReplaceTrash(Vector3Int trashPosOnTilemap, GameObject trashObject, TrashType trashType)
    {
        yield return new WaitForSeconds(0.15f); // Add a delay before placing the trash
        Destroy(trashObject);
        if (trashType == TrashType.None)
        {
            _gridInfo.SetPositionProperty(trashPosOnTilemap, "objectInstance", (Object)null);
        }
        else
        {
            FindObjectOfType<TrashSpawner>().Spawn(trashPosOnTilemap, trashType);
        }
    }
}