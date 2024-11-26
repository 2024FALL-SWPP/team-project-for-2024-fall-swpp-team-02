using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private Grid mapGrid;
    [SerializeField] private Grid obstacleGrid;
    [SerializeField] private float cooldown = 0.5f;
    
    public ScoreUI scoreUI;
    public BatteryUI batteryUI;
    
    private int life = 3;

    private Tilemap _obstacleTilemap;
    private bool _isInCooldown;

    private const float _respawnZAdd = 7.0f;
    private const float _respawnX = 7.5f;
    [SerializeField] private float referenceSpeed = 0.5f; 

    private float _goalZ;
    private float _startZ;
    private float _startTime;

    private int trashCount = 0;
    private int totalTrashes = 0;
    private int score = 0;
    
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
        if (transform.position.z >= _goalZ)
        {
            int level = DataManager.Instance.GetActiveLevelData().level;
            int score = CalculateFinalScore();
            ActiveLevelData levelClearData = new ActiveLevelData(level, score);
            DataManager.Instance.SetActiveLevelData(levelClearData);
            StageManager.Instance.GameClear();
        }
    }

    private IEnumerator CooldownRoutine()
    {
        yield return new WaitForSeconds(cooldown);
        _isInCooldown = false;
    }

    private void Start()
    {
        _obstacleTilemap = obstacleGrid.GetComponentInChildren<Tilemap>();
        // goalZ = StageManager.Instance.GetGoalZ();
        _goalZ = 24.5f; //for test map
        _startZ = transform.position.z;
        _startTime = Time.time;
    }

    private void Update()
    {
        UpdateScore();
    }

    private void DecreaseLife()
    {
        life--;
        batteryUI.UpdateBattery(life);
        Debug.Log("Life: " + life);

        if (life <= 0)
            StageManager.Instance.GameOver();
    }

    private void UpdateScore()
    {
        float currentZ = transform.position.z;
        float offsetZ = currentZ - _startZ;
        float referenceTime = offsetZ / referenceSpeed;
        float playTime = Time.time - _startTime;
        score = (int)(referenceTime - playTime + 5 * trashCount);
        scoreUI.UpdateScore(score);
    }

    private int CalculateFinalScore()
    {
        float currentZ = transform.position.z;
        float offsetZ = currentZ - _startZ;
        float referenceTime = offsetZ / referenceSpeed;
        float playTime = Time.time - _startTime;
        float clampedTimeScore = Mathf.Clamp(referenceTime - referenceTime, -20.0f, 20.0f);
        return (int)(clampedTimeScore + 5 * (trashCount - totalTrashes) + 100);
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
