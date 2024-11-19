using System.Collections.Generic;

public class PlayerBag
{
    private Queue<(TrashType, TrashSubtype)> _bag;
    private int _size;
    private (TrashType, TrashSubtype) _none = (TrashType.None, TrashSubtype.None);

    public PlayerBag(int bagSize)
    {
        _bag = new Queue<(TrashType, TrashSubtype)>(bagSize);
        _size = bagSize;
    }
    
    public (TrashType, TrashSubtype) GetFirstTrash()
    {
        return _bag.TryDequeue(out var trash) ? trash : _none;
    }
    
    public (TrashType, TrashSubtype) AddTrash(TrashType trashType, TrashSubtype trashSubtype)
    {
        return AddTrash((trashType, trashSubtype));
    }

    public (TrashType, TrashSubtype) AddTrash((TrashType, TrashSubtype) trash)
    {
        var overflow = _bag.Count >= _size ? _bag.Dequeue() : _none;
        _bag.Enqueue(trash);
        
        return overflow;
    }
    
    public (TrashType, TrashSubtype) CheckTrash()
    {
        return _bag.TryPeek(out var trash) ? trash : _none;
    }
}
