using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private Grid mapGrid;
    [SerializeField] private Grid obstacleGrid;
    [SerializeField] private float cooldown = 0.5f;
    [SerializeField] private float moveSpeed = 2.0f;
    [SerializeField] private float rotationSpeed = 10.0f;

    private int life = 3;

    private Tilemap _obstacleTilemap;
    private bool _isInCooldown;
    private bool _isWalking;
    private Vector3 _targetPosition;

    private Animator _animator; 

    private const float _respawnZAdd = 7.0f;
    private const float _respawnX = 7.5f;

    private void Start()
    {
        _obstacleTilemap = obstacleGrid.GetComponentInChildren<Tilemap>();
        _animator = GetComponent<Animator>();
        _targetPosition = transform.position; // Initialize target position
    }

    private void Update()
    {
        // Smooth movement toward the target position
        if (_isWalking)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, moveSpeed * Time.deltaTime);

            // Stop walking when reaching the target position
            if (Vector3.Distance(transform.position, _targetPosition) < 0.01f)
            {
                _isWalking = false;
                _animator.SetBool("isWalking", false); // Stop walk animation
            }
        }
    }

    /// <summary>
    /// Moves player 1 block to the given position.
    /// If an obstacle is in front of the player, the command doesn't work.
    /// </summary>
    /// <param name="direction">Direction you want to move player to.</param>
    public void Move(Direction direction)
    {
        var cellPos = mapGrid.WorldToCell(transform.position + direction.Value);
        if (_isInCooldown || _obstacleTilemap.HasTile(cellPos)) return;  // Condition check

        _targetPosition = transform.position + direction.Value; // Set target position
        _isWalking = true; // Start walking
        _animator.SetBool("isWalking", true); // Trigger walk animation

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

    private void DecreaseLife()
    {
        life--;
        Debug.Log("Life: " + life);

        if (life <= 0)
            StageManager.Instance.GameOver();
    }

    public void Respawn()
    {
        DecreaseLife();

        transform.position = new Vector3(_respawnX, transform.position.y, transform.position.z + _respawnZAdd);

        // If there's an obstacle on the respawn position, move to the nearest empty block
        int dx = 1;
        while (_obstacleTilemap.HasTile(mapGrid.WorldToCell(transform.position)))
        {
            transform.position += new Vector3(dx, 0, 0);
            if (dx > 0) dx = -dx - 1;
            else dx = -dx + 1;
        }
    }
}
