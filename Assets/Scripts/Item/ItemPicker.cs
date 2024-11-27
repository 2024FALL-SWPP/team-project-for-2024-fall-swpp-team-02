using UnityEngine;
using UnityEngine.Tilemaps;

public class ItemPicker : MonoBehaviour
{
    [SerializeField] private Grid itemGrid;

    private Tilemap _itemTilemap;
    private GridInformation _itemGridInfo;

    private GameObject FindItemAtCurrentPos()
    {
        var playerPosOnTilemap = _itemTilemap.WorldToCell(transform.position);
        return _itemGridInfo.GetPositionProperty<GameObject>(playerPosOnTilemap, "itemObject", null);
    }

    private bool CheckIfItemExists()
    {
        return _itemTilemap.HasTile(_itemTilemap.WorldToCell(transform.position));
    }
    
    private void Start()
    {
        _itemTilemap = itemGrid.GetComponentInChildren<Tilemap>();
        _itemGridInfo = itemGrid.GetComponent<GridInformation>();
    }

    private void Update()
    {
        if (!CheckIfItemExists()) return;
        
        var itemObject = FindItemAtCurrentPos();
        if (itemObject == null) return;
        
        var itemBehaviour = itemObject.GetComponent<IItemBehaviour>();
        itemBehaviour.OnPickup();
        
        var itemPosOnTilemap = _itemTilemap.WorldToCell(itemObject.transform.position);
        Destroy(itemObject);
        _itemTilemap.SetTile(itemPosOnTilemap, null);
    }
}
