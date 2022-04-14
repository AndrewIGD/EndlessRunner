using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private void Update()
    {
        transform.position -= transform.forward * GameManager.gameSpeed * GameManager.instance.levelData.platformSpeedMultiplier * Time.deltaTime * GameManager.instance.levelData.platformSpeed;
    }
}
