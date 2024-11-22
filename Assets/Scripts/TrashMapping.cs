using UnityEngine;

[CreateAssetMenu(fileName = "TrashMapping", menuName = "Custom Storage/Trash Mapping")]
public class TrashMapping : ScriptableObject
{
    public TrashMappingItem[] items;

    public TrashType SubtypeToType(TrashSubtype subtype)
    {
        return TrashType.None;
    }
}
