using Unity.VisualScripting;
using UnityEngine;

public class TrashInfo : MonoBehaviour
{
    public TrashType trashType;

    public static string TrashColor(TrashType trashType)
    {
        return trashType switch
        {
            TrashType.PaperGrouped => "Yellow",
            TrashType.PaperSingle1 => "Yellow",
            TrashType.PaperSingle2 => "Yellow",
            TrashType.CanHorizontal => "Red",
            TrashType.CanVertical => "Red",
            TrashType.PetBottleHorizontal => "Blue",
            TrashType.PetBottleVertical => "Blue",
            TrashType.Banana => "Green",
            TrashType.None => "None",
            _ => throw new UnexpectedEnumValueException<TrashType>(trashType),
        };
    }
}
