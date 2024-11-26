
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI: MonoBehaviour
{
    [SerializeField] private List<Image> inventorySlots;
    private Dictionary<TrashType, Sprite> trashSprites;
    private const string BaseSpritePath = "Sprites/TrashEntries/";

    private void Start()
    {
        InitializeTrashSprites();
        ClearInventory();
    }
    
    private void InitializeTrashSprites()
    {
        trashSprites = new Dictionary<TrashType, Sprite>
        {
            { TrashType.PaperGrouped, LoadSprite("Paper_Group") },
            { TrashType.PaperSingle1, LoadSprite("Paper_Single1") },
            { TrashType.PaperSingle2, LoadSprite("Paper_Single2") },
            { TrashType.CanHorizontal, LoadSprite("Can_Horizontal") },
            { TrashType.CanVertical, LoadSprite("Can_Vertical") },
            { TrashType.PetBottleHorizontal, LoadSprite("Bottle_Horizontal") },
            { TrashType.PetBottleVertical, LoadSprite("Bottle_Vertical") },
            { TrashType.Banana, LoadSprite("Banana") }
        };
    }
    
    private Sprite LoadSprite(string spriteName)
    {
        return Resources.Load<Sprite>($"{BaseSpritePath}{spriteName}");
    }
    
    public void UpdateInventory(List<TrashType> bag)
    {
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            inventorySlots[i].sprite = null;
            inventorySlots[i].enabled = false;
        }

        // Update inventory with new images
        int index = 0;
        foreach (var trash in bag)
        {
            if (index >= inventorySlots.Count) break; // Prevent overflow

            if (trashSprites.ContainsKey(trash))
            {
                inventorySlots[index].sprite = trashSprites[trash]; 
                inventorySlots[index].enabled = true; 
            }

            index++;
        }
    }

    public void ClearInventory()
    {
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            inventorySlots[i].sprite = null;
            inventorySlots[i].enabled = false; 
        }
    }
}
