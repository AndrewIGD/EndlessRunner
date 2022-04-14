using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighscoreCount : MonoBehaviour
{
    private void Start()
    {
        GetComponent<TextMeshProUGUI>().text = "Current Highscore: " + PlayerPrefs.GetInt("Highscore", 0);
    }
}
