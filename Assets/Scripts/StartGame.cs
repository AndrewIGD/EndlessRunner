using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartGame : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] AudioSource click;

    private bool _gameStarted = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_gameStarted)
            return;

        GameManager.instance.Activate();

        _gameStarted = true;

        click.Play();
    }
}
