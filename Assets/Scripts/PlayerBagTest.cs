using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerBagTest
{
    // A Test behaves as an ordinary method
    [Test]
    public void PlayerBagTestSimplePasses()
    {

        PlayerBag playerBag = new(3);
        Assert.AreEqual(playerBag.CheckTrash(), TrashType.None);
        playerBag.AddTrash(TrashType.Banana);
        Assert.AreEqual(playerBag.CheckTrash(), TrashType.Banana);
        TrashType trash = playerBag.RemoveTrash();
        Assert.AreEqual(trash, TrashType.Banana);
        Assert.AreEqual(playerBag.CheckTrash(), TrashType.None);
        trash = playerBag.AddTrash(TrashType.CanHorizontal);
        Assert.AreEqual(trash, TrashType.None);
        trash = playerBag.AddTrash(TrashType.CanVertical);
        Assert.AreEqual(trash, TrashType.None);
        trash = playerBag.AddTrash(TrashType.PaperGrouped);
        Assert.AreEqual(trash, TrashType.None);
        trash = playerBag.AddTrash(TrashType.PaperSingle1);
        Assert.AreEqual(trash, TrashType.CanHorizontal);
        List<TrashType> trashList = playerBag.GetTrashList();
        Assert.AreEqual(trashList.Count, 3);
        Assert.AreEqual(trashList[0], TrashType.CanVertical);
        Assert.AreEqual(trashList[1], TrashType.PaperGrouped);
        Assert.AreEqual(trashList[2], TrashType.PaperSingle1);
        playerBag.RotateBag();
        trashList = playerBag.GetTrashList();
        Assert.AreEqual(trashList.Count, 3);
        Assert.AreEqual(trashList[0], TrashType.PaperGrouped);
        Assert.AreEqual(trashList[1], TrashType.PaperSingle1);
        Assert.AreEqual(trashList[2], TrashType.CanVertical);

    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator PlayerBagTestWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
