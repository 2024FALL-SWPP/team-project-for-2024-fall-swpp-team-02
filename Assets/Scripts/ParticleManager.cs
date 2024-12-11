using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public ParticleSystem respawnEffect;
    public ParticleSystem disposeEffect;
    public ParticleSystem clearEffect;

    public void PlayRespawnEffect(Vector3 position)
    {
        if (respawnEffect != null)
        {
            Instantiate(respawnEffect, position, Quaternion.identity).Play();
        }
    }

    public void PlayDisposeEffect(Vector3 position)
    {
        if (disposeEffect != null)
        {
            Instantiate(disposeEffect, position, Quaternion.identity).Play();
        }
    }

    public IEnumerator PlayLevelClearEffect(Vector3 position)
    {
        if (clearEffect != null)
        {
            Instantiate(clearEffect, position, Quaternion.identity).Play();
        }
        yield return new WaitForSeconds(1.5f);
    }

}
