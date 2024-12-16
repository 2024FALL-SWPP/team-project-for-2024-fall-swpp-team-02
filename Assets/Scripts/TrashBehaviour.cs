using UnityEngine;

public class TrashBehaviour : MonoBehaviour
{
    public TrashType trashType;
    public ParticleSystem disposeEffect;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CameraBoundary"))
        {
            ScoreModel.Instance.IncTrashMissCount();
            FindObjectOfType<PlayerBehaviour>().DecreaseLife();
            Instantiate(disposeEffect, transform.position, Quaternion.identity).Play();
            Destroy(gameObject);
        }
    }
}
