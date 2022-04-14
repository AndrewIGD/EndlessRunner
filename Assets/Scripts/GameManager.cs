using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] int platformCount;
    [SerializeField] GameObject startPlatform;
    [SerializeField] Player player;
    [SerializeField] Animator canvas;
    [SerializeField] Animator volume;
    [SerializeField] Animator highscoreCanvas;
    [SerializeField] Animator loseCanvas;
    [SerializeField] TextMeshProUGUI highscoreCounter;
    [SerializeField] TextMeshProUGUI loseHighscoreCounter;

    public LevelData levelData;

    public static GameManager instance;

    public static float gameSpeed = 1f;

    public static bool active = false;

    private GameObject _lastPlatform = null;

    private Transform lastEndConnector;

    private bool _removeLeftMissing = true;

    private int _highscore = 0;

    public void IncrementHighscore()
    {
        highscoreCounter.text = (++_highscore).ToString();
    }

    public void Activate()
    {
        active = true;

        player.StartMoving();
        UIFadeOut();

        gameSpeed = levelData.startGameSpeed;

        Time.timeScale = gameSpeed;

        StartCoroutine(IncreaseGameSpeed());
    }

    public void SpawnPlatform()
    {
        List<GameObject> platformList = levelData.platforms.ToList();

        if (_lastPlatform != null)
            platformList.Remove(_lastPlatform);

        if(_removeLeftMissing)
        {
            for(int i=0;i<platformList.Count;i++)
                if(platformList[i].name.Trim() == "LeftMissing")
                {
                    platformList.RemoveAt(i);

                    break;
                }

            _removeLeftMissing = false;
        }

        _lastPlatform = platformList[Random.Range(0, platformList.Count)];

        GameObject platform = ObjectPool.instance.Get(_lastPlatform);
        Transform startConnector = platform.transform.GetChildWithName("StartConnector");

        Vector3 offset = startConnector.position - lastEndConnector.position;

        platform.transform.position -= offset;

        lastEndConnector = platform.transform.GetChildWithName("EndConnector");
    }

    public void End()
    {
        int previousHighscore = PlayerPrefs.GetInt("Highscore", 0);

        AssignHighscore(previousHighscore);

        LoseTransition();
    }

    private void UIFadeOut()
    {
        canvas.Play("fade");
        volume.Play("fade");
        highscoreCanvas.Play("fade");
    }

    private void LoseTransition()
    {
        highscoreCanvas.Play("fadeOut");

        loseCanvas.Play("fade");
    }

    private void AssignHighscore(int previousHighscore)
    {
        if (_highscore > previousHighscore)
        {
            PlayerPrefs.SetInt("Highscore", _highscore);

            loseHighscoreCounter.text = "New Highscore: " + _highscore;
        }
        else loseHighscoreCounter.text = "Highscore: " + _highscore;
    }

    private IEnumerator IncreaseGameSpeed()
    {
        while(true)
        {
            yield return new WaitForSecondsRealtime(levelData.timeBetweenIncrements);

            gameSpeed += levelData.gameSpeedIncrement;

            Time.timeScale = gameSpeed;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < levelData.platforms.Length; i++)
            ObjectPool.instance.Initialize(levelData.platforms[i]);

        lastEndConnector = startPlatform.transform.GetChildWithName("EndConnector");
        for (int i = 0; i < platformCount; i++)
            SpawnPlatform();
    }
}
