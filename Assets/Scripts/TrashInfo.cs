using UnityEngine;

public class TrashInfo : MonoBehaviour
{
    public TrashType trashType;

    public static string TrashColor(TrashType trashType)
    {
        switch (trashType)
        {
            case TrashType.PaperGrouped: return "Yellow";
            case TrashType.PaperSingle1: return "Yellow";
            case TrashType.PaperSingle2: return "Yellow";
            case TrashType.CanHorizontal: return "Red";
            case TrashType.CanVertical: return "Red";
            case TrashType.PetBottleHorizontal: return "Blue";
            case TrashType.PetBottleVertical: return "Blue";
            case TrashType.Banana: return "Green";
        }
        return "";
    }
}
