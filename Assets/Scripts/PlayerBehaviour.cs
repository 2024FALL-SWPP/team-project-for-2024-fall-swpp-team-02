using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private Grid mapGrid;
    [SerializeField] private Grid obstacleGrid;
    
    private Animator _animator;
    private Tilemap _obstacleTilemap;
    
    /// <summary>
    /// Moves player 1 block to the given position.
    /// If player is already moving, or an obstacle is in front of the player, the command doesn't work.
    /// </summary>
    /// <param name="direction">Direction you want to move player to.</param>
    public void Move(Direction direction)
    {
        if (!_animator && _animator.IsInTransition(0)) return;  // Condition check: Is player moving
        
        var directionVec = direction switch
        {
            Direction.Left => new Vector3(-1, 0, 0),
            Direction.Right => new Vector3(1, 0, 0),
            Direction.Back => new Vector3(0, 0, -1),
            Direction.Front => new Vector3(0, 0, 1),
            _ => Vector3.zero
        };

        if (Physics.Raycast(transform.position, directionVec, 1.0f)) return;  // Condition check: Is tile occupied by obstacle

        switch (direction)
        {
            case Direction.Left:
                _animator.SetTrigger("JumpLeft");
                break;
            case Direction.Right:
                _animator.SetTrigger("JumpRight");
                break;
            case Direction.Back:
                _animator.SetTrigger("JumpBack");
                break;
            case Direction.Front:
                _animator.SetTrigger("JumpFront");
                break;
        }
        
        StartCoroutine(UpdatePosCoroutine(direction, 0.51f));
    }

    /// <summary>
    /// Updates player's position.
    /// </summary>
    /// <param name="direction">Direction which player moves to.</param>
    public void UpdatePos(Direction direction)
    {
        transform.position += direction switch
        {
            Direction.Left => new Vector3(-1, 0, 0),
            Direction.Right => new Vector3(1, 0, 0),
            Direction.Back => new Vector3(0, 0, -1),
            Direction.Front => new Vector3(0, 0, 1),
            _ => Vector3.zero
        };
    }
    
    private IEnumerator UpdatePosCoroutine(Direction direction, float delay)
    {
        yield return new WaitForSeconds(delay);
        UpdatePos(direction);
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        
        _obstacleTilemap = obstacleGrid.GetComponentInChildren<Tilemap>();
    }
}
