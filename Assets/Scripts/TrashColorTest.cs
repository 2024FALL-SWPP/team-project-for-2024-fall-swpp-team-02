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
        Assert.AreEqual(TrashInfo.TrashColor(TrashType.PaperGrouped), "Yellow");
        Assert.AreEqual(TrashInfo.TrashColor(TrashType.PaperSingle1), "Yellow");
        Assert.AreEqual(TrashInfo.TrashColor(TrashType.PaperSingle2), "Yellow");
        Assert.AreEqual(TrashInfo.TrashColor(TrashType.CanHorizontal), "Red");
        Assert.AreEqual(TrashInfo.TrashColor(TrashType.CanVertical), "Red");
        Assert.AreEqual(TrashInfo.TrashColor(TrashType.PetBottleHorizontal), "Blue");
        Assert.AreEqual(TrashInfo.TrashColor(TrashType.PetBottleVertical), "Blue");
        Assert.AreEqual(TrashInfo.TrashColor(TrashType.Banana), "Green");
        Assert.AreEqual(TrashInfo.TrashColor(TrashType.None), "");
    }

}
