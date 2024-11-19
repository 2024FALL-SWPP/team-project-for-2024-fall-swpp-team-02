using System.Collections.Generic;

public class PlayerBagController
{
    private PlayerBag _playerBag;
    
    public PlayerBagController(int bagSize)
    {
        _playerBag = new PlayerBag(bagSize);
    }

    public TrashType GetFirstTrashType()
    {
        return _playerBag.CheckTrash().Item1;
    }

    public TrashSubtype GetFirstTrashSubtype()
    {
        return _playerBag.CheckTrash().Item2;
    }

    public List<(TrashType, TrashSubtype)> GetTrashList()
    {
        List<(TrashType, TrashSubtype)> trashList = new();
        (TrashType, TrashSubtype) buffer;

        while ((buffer = _playerBag.GetFirstTrash()).Item1 != TrashType.None)
            trashList.Add(buffer);

        foreach (var item in trashList)
            _playerBag.AddTrash(item);

        return trashList;
    }

    public void AddTrash(TrashType trashType, TrashSubtype trashSubtype)
    {
        _playerBag.AddTrash(trashType, trashSubtype);
    }
}
