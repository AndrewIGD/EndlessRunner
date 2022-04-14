using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/LevelData")]
public class LevelData : ScriptableObject
{
    public float startGameSpeed;
    public float gameSpeedIncrement;
    public float timeBetweenIncrements;
    public float platformSpeed;
    public float platformSpeedMultiplier;
    public GameObject[] platforms;
}
