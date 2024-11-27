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

    public TrashType GetFirstTrash()
    {
        return _bag.TryDequeue(out var trash) ? trash : TrashType.None;
    }

    public TrashType AddTrash(TrashType trash)
    {
        var overflow = _bag.Count >= _size ? _bag.Dequeue() : TrashType.None;
        _bag.Enqueue(trash);

        return overflow;
    }

    public TrashType CheckTrash()
    {
        return _bag.TryPeek(out var trash) ? trash : TrashType.None;
    }

    public List<TrashType> GetTrashList() => _bag.ToList();

    public TrashType RemoveTrash()
    {
        if (_bag.TryPeek(out var firstTrash)) return TrashType.None;
        return _bag.Dequeue();
    }

    public void RotateBag()
    {
        var firstTrash = _bag.Dequeue();
        _bag.Enqueue(firstTrash);
    }
}
