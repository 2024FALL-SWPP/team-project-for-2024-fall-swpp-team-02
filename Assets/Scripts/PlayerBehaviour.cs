using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private Grid mapGrid;
    [SerializeField] private Grid obstacleGrid;
    [SerializeField] private float moveCooldown = 0.5f;
    [SerializeField] private float rotateCooldown = 0.3f;
    [SerializeField] private float trashDisposeCooldown = 0.3f;

    public ScoreUI scoreUI;
    public BatteryUI batteryUI;

    [SerializeField] private float moveSpeed = 2.0f;
    [SerializeField] private float rotationSpeed = 10.0f;

    private int life = 3;

    private Tilemap _obstacleTilemap;
    private bool _isInCooldown = false;
    private bool _isInTrashDisposeCooldown = false;
    private bool _isWalking = false;
    private Vector3 _targetPosition;
    private Vector3 _targetDirection;

    private Animator _animator;

    private const float _respawnZAdd = 7.0f;
    private const float _respawnX = 7.5f;

    private Direction direction;

    [SerializeField] private float referenceSpeed = 0.5f;

    private TrashMapping _trashMapping;

    private float goalZ;

    private void Start()
    {
        _obstacleTilemap = obstacleGrid.GetComponentInChildren<Tilemap>();
        _animator = GetComponent<Animator>();
        _targetPosition = transform.position;
        _targetDirection = Vector3.zero;
        _trashMapping = Resources.Load("Storages/TrashMapping") as TrashMapping;

        goalZ = StageManager.Instance.GetGoalZ();
        float _startZ = transform.position.z;
        ScoreModel.Instance = new ScoreModel(_startZ, referenceSpeed, scoreUI);
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
                QuantizeRotation();
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
            ClearGame();
        }
    }

    private void ClearGame()
    {
        int level = DataManager.Instance.GetActiveLevelData().level;
        int score = ScoreModel.Instance.CalculateFinalScore(transform.position.z);
        ActiveLevelData levelClearData = new ActiveLevelData(level, score);
        DataManager.Instance.SetActiveLevelData(levelClearData);
        StageManager.Instance.GameClear();
    }

    /// <summary>
    /// Moves player 1 block to the given position.
    /// If an obstacle is in front of the player, the command doesn't work.
    /// </summary>
    /// <param name="direction">Direction you want to move player to.</param>
    public void Move(Direction direction)
    {
        var cellPos = mapGrid.WorldToCell(transform.position + direction.Value);
        if (_isInCooldown) return;
        if (_obstacleTilemap.HasTile(cellPos))
        {
            Rotate(direction);
            return;
        }
        _isInCooldown = true;

        this.direction = direction;

        _targetPosition = transform.position + direction.Value;
        _targetDirection = direction.Value;
        _isWalking = true;
        _animator.SetBool("isWalking", true);

        StartCoroutine(nameof(MoveCooldownRoutine));
    }

    public void Rotate(Direction direction)
    {
        if (_isInCooldown) return;
        _isInCooldown = true;
        if (this.direction.Equals(direction))
        {
            AudioManager.Instance.PlaySFX("MotionFail");
        }
        else
        {
            this.direction = direction;
            _targetDirection = direction.Value;
        }
        StartCoroutine(nameof(RotateCooldownRoutine));
    }

    // Temporary function set the trigger "triggerThrow" and "triggerPickUp"
    public void TriggerThrowAnimation()
    {
        _animator.Play("InLevel.Throw");

    }
    public void TriggerPickUpAnimation()
    {
        _animator.Play("InLevel.Pick Up");
    }

    private IEnumerator MoveCooldownRoutine()
    {
        yield return new WaitForSeconds(moveCooldown);
        _isInCooldown = false;
    }

    private IEnumerator RotateCooldownRoutine()
    {
        yield return new WaitForSeconds(rotateCooldown);
        QuantizeRotation();
        _targetDirection = Vector3.zero;
        _isInCooldown = false;
    }

    private IEnumerator TrashDisposeCooldownRoutine()
    {
        yield return new WaitForSeconds(trashDisposeCooldown);
        _isInTrashDisposeCooldown = false;
    }

    public bool DecreaseLife()
    {
        life--;
        batteryUI.UpdateBattery(life);

        if (life <= 0)
        {
            StageManager.Instance.GameOver();
            return true;
        }

        AudioManager.Instance.PlaySFX("PlayerHurt");
        return false;
    }

    // Should be removed after moving life field out from PlayerBehaviour
    public void IncreaseLife(int amount)
    {
        life += amount;
        if (life > 3)
            life = 3;
        batteryUI.UpdateBattery(life);

        AudioManager.Instance.PlaySFX("PlayerRestoreLife");
    }

    public void Respawn()
    {
        if (DecreaseLife())
            return;

        var newPos = new Vector3(_respawnX, transform.position.y, transform.position.z + _respawnZAdd);
        if (newPos.z >= goalZ)
        {
            newPos.z = goalZ;
        }
        transform.position = newPos;

        // If respawn position is past goal, clear game
        if (transform.position.z >= goalZ)
        {
            ClearGame();
            return;
        }

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
        if (_targetDirection == Vector3.zero) return;
        transform.rotation = Quaternion.LookRotation(_targetDirection);
    }

    public void RotateBag()
    {
        StageManager.Instance.bagController.RotateBag();
        AudioManager.Instance.PlaySFX("BagRotate");
    }

    public void DisposeTrash()
    {
        if (_isInTrashDisposeCooldown) return;
        _isInTrashDisposeCooldown = true;

        var trashType = StageManager.Instance.bagController.GetFirstTrashType();
        if (trashType == TrashType.None)
        {
            AudioManager.Instance.PlaySFX("MotionFail");
            StartCoroutine(nameof(TrashDisposeCooldownRoutine));
            return;
        }
        var trashColor = _trashMapping.TrashColor(trashType);

        var frontPos = mapGrid.WorldToCell(transform.position + direction.Value);
        var frontObstacle = _obstacleTilemap.GetTile(frontPos);

        if (frontObstacle == null || !frontObstacle.name.StartsWith(trashColor))
        {
            AudioManager.Instance.PlaySFX("MotionFail");
            StartCoroutine(nameof(TrashDisposeCooldownRoutine));
            return;
        }
        TriggerThrowAnimation();
        AudioManager.Instance.PlaySFX("TrashDispose");
        while (trashType != TrashType.None && _trashMapping.TrashColor(trashType) == trashColor)
        {
            StageManager.Instance.bagController.RemoveTrash();
            trashType = StageManager.Instance.bagController.GetFirstTrashType();
        }

        StartCoroutine(nameof(TrashDisposeCooldownRoutine));
    }
}
