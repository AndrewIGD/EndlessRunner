using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPad : MonoBehaviour
{
    [SerializeField] AudioSource jumpBoost;

    private void OnCollisionEnter(Collision collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();

        if (player != null)
            jumpBoost.Play();
    }
}
