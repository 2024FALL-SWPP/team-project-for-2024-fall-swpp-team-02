using System;
using System.Collections.Generic;
using System.Linq;

public class PlayerBag
{
    private Queue<TrashType> _bag;
    private int _size;

    public PlayerBag(int bagSize)
    {
        _bag = new Queue<TrashType>(bagSize);
        _size = bagSize;
    }

    public TrashType AddTrash(TrashType trash)
    {
        var overflow = _bag.Count >= _size ? _bag.Dequeue() : TrashType.None;
        _bag.Enqueue(trash);
        ScoreModel.Instance?.IncTrashPickupCount();
        UpdateUI();
        return overflow;
    }

    public TrashType CheckTrash()
    {
        return _bag.TryPeek(out var trash) ? trash : TrashType.None;
    }

    public List<TrashType> GetTrashList() => _bag.ToList();

    public TrashType RemoveTrash()
    {
        if (!_bag.TryPeek(out var firstTrash)) return TrashType.None;
        var trash = _bag.Dequeue();
        ScoreModel.Instance?.IncTrashDisposeCount();
        UpdateUI();
        return trash;
    }

    public void RotateBag()
    {
        if (_bag.Count <= 1) return;
        var firstTrash = _bag.Dequeue();
        _bag.Enqueue(firstTrash);
        UpdateUI();
    }

    internal bool IsBagFull()
    {
        return _bag.Count >= _size;
    }

    private void UpdateUI()
    {
        if (InventoryUI.Instance == null) return;
        InventoryUI.Instance.UpdateInventory(GetTrashList());
    }
}
