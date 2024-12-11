using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerBagControllerTest
{
    // A Test behaves as an ordinary method
    [Test]
    public void PlayerBagControllerTestSimplePasses()
    {

        PlayerBagController playerBagController = new(3);
        Assert.AreEqual(playerBagController.GetFirstTrashType(), TrashType.None);
        playerBagController.AddTrash(TrashType.Banana);
        Assert.AreEqual(playerBagController.GetFirstTrashType(), TrashType.Banana);
        bool success = playerBagController.RemoveTrash();
        Assert.AreEqual(success, true);
        Assert.AreEqual(playerBagController.GetFirstTrashType(), TrashType.None);
        playerBagController.AddTrash(TrashType.CanHorizontal);
        playerBagController.AddTrash(TrashType.CanVertical);
        playerBagController.AddTrash(TrashType.PaperGrouped);
        playerBagController.AddTrash(TrashType.PaperSingle1);
        List<TrashType> trashList = playerBagController.GetTrashList();
        Assert.AreEqual(trashList.Count, 3);
        Assert.AreEqual(trashList[0], TrashType.CanVertical);
        Assert.AreEqual(trashList[1], TrashType.PaperGrouped);
        Assert.AreEqual(trashList[2], TrashType.PaperSingle1);
        playerBagController.RotateBag();
        trashList = playerBagController.GetTrashList();
        Assert.AreEqual(trashList.Count, 3);
        Assert.AreEqual(trashList[0], TrashType.PaperGrouped);
        Assert.AreEqual(trashList[1], TrashType.PaperSingle1);
        Assert.AreEqual(trashList[2], TrashType.CanVertical);
    }
}
