using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideFlipActivation : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();

        if (player != null)
            player.Sideflip();
    }

    private void OnTriggerExit(Collider other)
    {
        Player player = other.GetComponent<Player>();

        if (player != null)
            player.StopSideflip();
    }
}
