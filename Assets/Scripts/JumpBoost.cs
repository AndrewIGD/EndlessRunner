using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBoost : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();

        if (player != null)
            player.JumpBoost();
    }

    private void OnCollisionExit(Collision collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();

        if (player != null)
            player.CancelJumpBoost();
    }
}
