using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private PlayerBehaviour player;

    [SerializeField] private KeyCode playerLeftKey = KeyCode.A;
    [SerializeField] private KeyCode playerRightKey = KeyCode.D;
    [SerializeField] private KeyCode playerFrontKey = KeyCode.W;
    [SerializeField] private KeyCode playerBackKey = KeyCode.S;

    private void Update()
    {
        // Player movement
        if (Input.GetKey(playerLeftKey))
        {
            player.Move(Direction.Left);
            player.Rotate(Direction.Left);
        }

        if (Input.GetKey(playerRightKey))
        {
            player.Move(Direction.Right);
            player.Rotate(Direction.Right);
        }

        if (Input.GetKey(playerFrontKey))
        {
            player.Move(Direction.Front);
            player.Rotate(Direction.Front);
        }

        if (Input.GetKey(playerBackKey))
        {
            player.Move(Direction.Back);
            player.Rotate(Direction.Back);
        }
    }
}