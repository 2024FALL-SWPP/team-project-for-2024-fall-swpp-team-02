using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private Grid mapGrid;
    [SerializeField] private Grid obstacleGrid;
    [SerializeField] private float moveCooldown = 0.5f;
    [SerializeField] private float rotateCooldown = 0.3f;
    [SerializeField] private float moveSpeed = 2.0f;
    [SerializeField] private float rotationSpeed = 10.0f;

    private int life = 3;

    private Tilemap _obstacleTilemap;
    private bool _isInCooldown;
    private bool _isWalking = false;
    private Vector3 _targetPosition;
    private Vector3 _targetDirection;

    private Animator _animator;

    private const float _respawnZAdd = 9.0f;
    private const float _respawnX = 8.5f;
    [SerializeField] private float referenceSpeed = 0.5f;

    private float goalZ;
    
    private void Start()
    {
        _obstacleTilemap = obstacleGrid.GetComponentInChildren<Tilemap>();
        _animator = GetComponent<Animator>();
        _targetPosition = transform.position;
        _targetDirection = Vector3.zero;

        goalZ = StageManager.Instance.GetGoalZ();
        float _startZ = transform.position.z;
        ScoreModel.Instance = new ScoreModel(_startZ, referenceSpeed);
    }

    /// <summary>
    /// Handles the player's movement and rotation toward the target position.
    /// Manages the player's walking animation state.
    /// </summary>
    private void Update()
    {
        if (_isWalking)
        {
            // Move the player toward the target position
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, moveSpeed * Time.deltaTime);

            // Stop walking when reaching the target position
            if (Vector3.Distance(transform.position, _targetPosition) < 0.01f)
            {
                _targetDirection = Vector3.zero;
                _isWalking = false;
                _animator.SetBool("isWalking", false);
                
                QuantizePosition();
            }
        }
        
        // Rotate the player toward the direction of movement
        if (_targetDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(_targetDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        ScoreModel.Instance.UpdateScore(transform.position.z);

        if (transform.position.z >= goalZ)
        {
            int level = DataManager.Instance.GetActiveLevelData().level;
            int score = ScoreModel.Instance.CalculateFinalScore(transform.position.z);
            ActiveLevelData levelClearData = new ActiveLevelData(level, score);
            DataManager.Instance.SetActiveLevelData(levelClearData);
            StageManager.Instance.GameClear();
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
        if (_isInCooldown || _obstacleTilemap.HasTile(cellPos)) return;

        _targetPosition = transform.position + direction.Value;
        _targetDirection = direction.Value;
        _isWalking = true;
        _animator.SetBool("isWalking", true);

        _isInCooldown = true;
        StartCoroutine(nameof(MoveCooldownRoutine));
    }
    
    public void Rotate(Direction direction)
    {
        if (_isInCooldown) return;
        
        _targetDirection = direction.Value;
        _isInCooldown = true;
        StartCoroutine(nameof(RotateCooldownRoutine));
    }

    // Temporary function set the trigger "triggerThrow" and "triggerPickUp"
    public void TriggerThrowAnimation()
    {
        _animator.SetTrigger("triggerThrow");

    }
    public void TriggerPickUpAnimation()
    {
        _animator.SetTrigger("triggerPickUp");

    }

    private IEnumerator MoveCooldownRoutine()
    {
        yield return new WaitForSeconds(moveCooldown);
        _isInCooldown = false;
    }
    
    private IEnumerator RotateCooldownRoutine()
    {
        yield return new WaitForSeconds(rotateCooldown);
        _isInCooldown = false;
        _targetDirection = Vector3.zero;
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

        // Reset target position to player's respawned position, prevents player moving to previous target upon respawn
        _targetPosition = transform.position;

        // Stop walking animation
        _isWalking = false;
        _animator.SetBool("isWalking", false);
    }

    private void QuantizePosition()
    {
        var x = Mathf.Round(transform.position.x - 0.5f) + 0.5f;
        var z = Mathf.Round(transform.position.z - 0.5f) + 0.5f;
        
        transform.position = new Vector3(x, transform.position.y, z);
    }

    private void QuantizeRotation()
    {
        
    }
}
