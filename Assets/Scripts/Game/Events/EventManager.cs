using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;

public class EventManager : MonoBehaviour
{
    //Singleton
    private static EventManager _instance = null;
    public static EventManager Instance => _instance;
    //
    private Chrono _chrono;

    private Lightning _lightning;
    private Earthquake _eartquake;
    private Wind _wind;

    private Coroutine _eventsCoroutine;

    public bool isThereAnEventInProgress;

    private void Awake()
    {
        //Singleton
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            _instance = this;
        }
        //
    }

    private void Start()
    {
        _chrono = Chrono.Instance;
        _lightning = GetComponent<Lightning>();
        _eartquake = GetComponent<Earthquake>();
        _wind = GetComponent<Wind>();

        _chrono.EndOfTheGame += StopEvents;

        _eventsCoroutine = StartCoroutine(PlayEvent());
    }

    private IEnumerator PlayEvent()
    {
        int randomEvent = Random.Range(0, 3);
        int randomTime = Random.Range(5, 5);

        yield return new WaitForSeconds(randomTime);

        switch (randomEvent)
        {
            case 0:
                {
                    isThereAnEventInProgress = true;
                    _lightning.DoLightningStrike();
                    break;
                }
            case 1:
                {
                    isThereAnEventInProgress = true;
                    _eartquake.DoEarthquake();
                    break;
                }
            case 2:
                {
                    isThereAnEventInProgress = true;
                    _wind.DoWindBlow();
                    break;
                }
        }

        _eventsCoroutine = StartCoroutine(PlayEvent());
    }

    private void StopEvents()
    {
        StopCoroutine(_eventsCoroutine);
        _eventsCoroutine = null;
    }
}