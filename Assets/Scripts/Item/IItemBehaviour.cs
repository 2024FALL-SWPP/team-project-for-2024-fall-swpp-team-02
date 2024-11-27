public interface IItemBehaviour
{
    public ItemType ItemType { get; set; }
    public void OnPickup();
}
