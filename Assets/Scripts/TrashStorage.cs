using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "TrashStorage", menuName = "Custom Storage/Trash Storage", order = 1)]
public class TrashStorage : ScriptableObject
{
    public List<GameObject> trashList = new();

    public void AddTrash(GameObject trashObject)
    {
        trashList.Add(trashObject);
    }

    public GameObject SearchByPosition(Vector3 pos)
    {
        foreach (var trash in trashList)
            if (trash.transform.position == pos) return trash;

        return null;
    }

    public void RemoveTrash(GameObject trashObject)
    {
        trashList.Remove(trashObject);
        Destroy(trashObject);
    }

    public void Prune()
    {
        trashList.Clear();
    }
}