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
        return _playerBag.CheckTrash();
    }

    public List<TrashType> GetTrashList()
    {
        return _playerBag.GetTrashList();
    }

    public void AddTrash(TrashType trashType)
    {
        _playerBag.AddTrash(trashType);
    }
}
