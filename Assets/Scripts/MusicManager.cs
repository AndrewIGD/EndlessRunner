using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private void Start()
    {
        if (FindObjectsOfType<MusicManager>().Length > 1)
            Destroy(gameObject);
        else
        {
            transform.parent = null;
            DontDestroyOnLoad(gameObject);
        }
    }
}
