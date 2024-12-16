using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private PlayerBehaviour player;

    [SerializeField] private KeyCode playerLeftKey = KeyCode.A;
    [SerializeField] private KeyCode playerRightKey = KeyCode.D;
    [SerializeField] private KeyCode playerFrontKey = KeyCode.W;
    [SerializeField] private KeyCode playerBackKey = KeyCode.S;
    [SerializeField] private KeyCode playerDisposeKey = KeyCode.Space;
    [SerializeField] private KeyCode playerRotateKey = KeyCode.Tab;

    private void Update()
    {
        if (StageManager.Instance.IsPaused()) return;
        // Player movement
        if (Input.GetKey(playerLeftKey) || Input.GetKey(KeyCode.LeftArrow))
        {
            player.Move(Direction.Left);
        }

        if (Input.GetKey(playerRightKey) || Input.GetKey(KeyCode.RightArrow))
        {
            player.Move(Direction.Right);
        }

        if (Input.GetKey(playerFrontKey) || Input.GetKey(KeyCode.UpArrow))
        {
            player.Move(Direction.Front);
        }

        if (Input.GetKey(playerBackKey) || Input.GetKey(KeyCode.DownArrow))
        {
            player.Move(Direction.Back);
        }
        if (Input.GetKey(playerDisposeKey)) player.DisposeTrash();
        if (Input.GetKeyDown(playerRotateKey)) player.RotateBag();
    }
}