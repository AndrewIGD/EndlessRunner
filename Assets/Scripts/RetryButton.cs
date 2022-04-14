using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class RetryButton : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] AudioSource click;

    public IEnumerator LoadScene()
    {
        yield return new WaitForSecondsRealtime(0.25f);

        SceneManager.LoadScene("MainScene");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        click.Play();

        StartCoroutine(LoadScene());
    }
}
