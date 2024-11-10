using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private Grid mapGrid;
    [SerializeField] private Grid obstacleGrid;
    [SerializeField] private float cooldown = 0.5f;
    
    private Tilemap _obstacleTilemap;
    private bool _isInCooldown;
    
    /// <summary>
    /// Moves player 1 block to the given position.
    /// If an obstacle is in front of the player, the command doesn't work.
    /// </summary>
    /// <param name="direction">Direction you want to move player to.</param>
    public void Move(Direction direction)
    {
        var cellPos = mapGrid.WorldToCell(transform.position + direction.Value);
        if (_isInCooldown || _obstacleTilemap.HasTile(cellPos)) return;  // Condition check

        UpdatePos(direction);
        _isInCooldown = true;
        StartCoroutine(nameof(CooldownRoutine));
    }

    /// <summary>
    /// Updates player's position.
    /// </summary>
    /// <param name="direction">Direction which player moves to.</param>
    public void UpdatePos(Direction direction)
    {
        transform.position += direction.Value;
    }

    private IEnumerator CooldownRoutine()
    {
        yield return new WaitForSeconds(cooldown);
        _isInCooldown = false;
    }

    private void Start()
    {
        _obstacleTilemap = obstacleGrid.GetComponentInChildren<Tilemap>();
    }
}
