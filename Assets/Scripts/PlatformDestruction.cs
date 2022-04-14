using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDestruction : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == Camera.main.gameObject)
        {
            GameManager.instance.IncrementHighscore();

            GameManager.instance.SpawnPlatform();

            ObjectPool.instance.Return(transform.parent.gameObject);

            if (transform.parent.gameObject.activeInHierarchy)
                Destroy(transform.parent.gameObject);
        }
    }
}
