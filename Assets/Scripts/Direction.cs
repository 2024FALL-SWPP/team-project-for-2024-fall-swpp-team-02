using UnityEngine;

public struct Direction
{
    public static readonly Direction Left = new(new Vector3(-1, 0, 0));
    public static readonly Direction Right = new(new Vector3(1, 0, 0));
    public static readonly Direction Front = new(new Vector3(0, 0, 1));
    public static readonly Direction Back = new(new Vector3(0, 0, -1));

    public Vector3 Value { get; private set; }

    private Direction(Vector3 direction)
    {
        Value = direction.normalized;
    }
}