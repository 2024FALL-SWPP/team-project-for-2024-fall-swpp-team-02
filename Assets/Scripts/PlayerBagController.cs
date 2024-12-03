using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PlayerBagController
{
    private PlayerBag _playerBag;

    public PlayerBagController(int bagSize)
    {
        _playerBag = new PlayerBag(bagSize);
    }

    public bool IsBagFull()
    {
        return _playerBag.IsBagFull();
    }

    public TrashType GetFirstTrashType()
    {
        return _playerBag.CheckTrash();
    }

    public List<TrashType> GetTrashList()
    {
        return _playerBag.GetTrashList();
    }

    public TrashType AddTrash(TrashType trashType)
    {
        return _playerBag.AddTrash(trashType);
    }

    public bool RemoveTrash()
    {
        if (_playerBag.RemoveTrash() != TrashType.None) return true;
        return false;
    }

    public void RotateBag()
    {
        _playerBag.RotateBag();
    }
}
