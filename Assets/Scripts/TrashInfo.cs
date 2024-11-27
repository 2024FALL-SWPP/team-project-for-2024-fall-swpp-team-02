using UnityEngine;

public class TrashInfo : MonoBehaviour
{
    public TrashType trashType;

    public static string TrashColor(TrashType trashType)
    {
        switch (trashType)
        {
            case TrashType.PaperGrouped: return "Blue";
            case TrashType.PaperSingle1: return "White";
            case TrashType.PaperSingle2: return "White";
            case TrashType.CanHorizontal: return "Red";
            case TrashType.CanVertical: return "Red";
            case TrashType.PetBottleHorizontal: return "Green";
            case TrashType.PetBottleVertical: return "Green";
            case TrashType.Banana: return "Yellow";
        }
        return "";
    }
}
