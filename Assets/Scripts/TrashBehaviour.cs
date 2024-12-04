using UnityEngine;

public class TrashBehaviour : MonoBehaviour
{
    public TrashType trashType;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CameraBoundary"))
        {
            ScoreModel.Instance.IncTrashMissCount();
            FindObjectOfType<PlayerBehaviour>().DecreaseLife();
            Destroy(gameObject);
        }
    }
}
