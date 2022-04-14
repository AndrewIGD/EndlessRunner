using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTrigger : MonoBehaviour
{
    private Player _player;

    private void Start()
    {
        _player = GetComponentInParent<Player>();
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Ground"))
            _player.Grounded();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ground"))
            _player.Falling();
    }
}
