using UnityEngine;

public class BatteryPackBehaviour : MonoBehaviour, IItemBehaviour
{
    public ItemType ItemType { get; set; }

    private GameObject _player;

    public void OnPickup()
    {
        _player.GetComponent<PlayerBehaviour>().IncreaseLife(1);
    }

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }
}
