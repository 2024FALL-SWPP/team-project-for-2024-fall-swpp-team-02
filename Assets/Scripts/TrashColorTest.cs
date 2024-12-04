using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TrashColorTest
{
    // A Test behaves as an ordinary method
    [Test]
    public void TrashColorPasses()
    {
        TrashMapping trashMapping = Resources.Load("Storages/TrashMapping") as TrashMapping;
        Assert.AreEqual(trashMapping.TrashColor(TrashType.PaperGrouped), "Yellow");
        Assert.AreEqual(trashMapping.TrashColor(TrashType.PaperSingle1), "Yellow");
        Assert.AreEqual(trashMapping.TrashColor(TrashType.PaperSingle2), "Yellow");
        Assert.AreEqual(trashMapping.TrashColor(TrashType.CanHorizontal), "Red");
        Assert.AreEqual(trashMapping.TrashColor(TrashType.CanVertical), "Red");
        Assert.AreEqual(trashMapping.TrashColor(TrashType.PetBottleHorizontal), "Blue");
        Assert.AreEqual(trashMapping.TrashColor(TrashType.PetBottleVertical), "Blue");
        Assert.AreEqual(trashMapping.TrashColor(TrashType.Banana), "Green");
        Assert.AreEqual(trashMapping.TrashColor(TrashType.None), "");
    }

}
