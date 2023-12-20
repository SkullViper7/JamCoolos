using System.Collections;
using UnityEngine;
using TMPro;
using System;

public class Chrono : MonoBehaviour
{
    public float time;

    public TMP_Text minutes;
    public TMP_Text seconds;
    private Coroutine decrementTimer;

    private int nbrOfMinutes;
    private int nbrOfSeconds;

    [Space]
    public GameObject EndScreen;
    public GameObject gameUI;
    public AudioSource gameMusic;
    public AudioSource endMusic;

    // Observer
    public delegate void ProgressDelegate();

    public event ProgressDelegate NewSecond;
    public event ProgressDelegate TiersOfTheGame;
    public event ProgressDelegate HalfOfTheGame;
    public event ProgressDelegate LastQuarterOfTheGame;
    public event ProgressDelegate EndOfTheGame;
    //

    private void Start()
    {
        ConvertTimeIntoChrono(time);

        minutes.SetText(ConvertToString(nbrOfMinutes));
        seconds.SetText(ConvertToString(nbrOfSeconds));

        decrementTimer = StartCoroutine(DecrementChrono());
    }

    private void ConvertTimeIntoChrono(float _time)
    {
        // Convert seconds into minutes and seconds
        nbrOfMinutes = Mathf.FloorToInt(_time / 60f);
        nbrOfSeconds = (int)(_time - (Mathf.FloorToInt(_time / 60f) * 60f));
    }

    private float ConvertChronoIntoTime(int _minutes, int _seconds)
    {
        // Convert seconds and minutes into seconds
        float _time = 0;

        _time += _minutes * 60;
        _time += _seconds;

        return _time;
    }

    private IEnumerator DecrementChrono()
    {
        CheckGameProgress();
        yield return new WaitForSeconds(1);

        // Decrement seconds at each second and minutes when seconds are under 0
        // Stop the chrono if minutes and seconds are equals to 0
        if (nbrOfSeconds - 1 == -1 && nbrOfMinutes != 0)
        {
            nbrOfSeconds = 59;
            nbrOfMinutes -= 1;

            minutes.SetText(ConvertToString(nbrOfMinutes));
            seconds.SetText(ConvertToString(nbrOfSeconds));
            decrementTimer = StartCoroutine(DecrementChrono());
        }
        else if (nbrOfSeconds - 1 == -1 && nbrOfMinutes == 0)
        {
            StopTimer();
        }
        else
        {
            nbrOfSeconds -= 1;
            seconds.SetText(ConvertToString(nbrOfSeconds));
            decrementTimer = StartCoroutine(DecrementChrono());
        }

        //Invoke this event at each second
        NewSecond?.Invoke();
    }

    private void CheckGameProgress()
    {
        //Check if it's the tiers of the game
        if (ConvertChronoIntoTime(nbrOfMinutes, nbrOfSeconds) == Math.Floor(time / 3) * 2)
        {
            TiersOfTheGame?.Invoke();
        }
        //Check if it's the half of the game
        else if (ConvertChronoIntoTime(nbrOfMinutes, nbrOfSeconds) == Math.Floor(time / 2))
        {
            HalfOfTheGame?.Invoke();
        }
        //Check if it's the last quarter of the game
        else if (ConvertChronoIntoTime(nbrOfMinutes, nbrOfSeconds) == Math.Floor(time / 4))
        {
            LastQuarterOfTheGame?.Invoke();
        }
    }

    public void StopTimer()
    {
        // Stop the timer
        StopCoroutine(decrementTimer);
        EndScreen.SetActive(true);
        gameUI.SetActive(false);
        gameMusic.Stop();
        endMusic.Play();
        GameManager.Instance.isGameOver = true;
        EndOfTheGame?.Invoke();
    }

    private string ConvertToString(int _time)
    {
        if (_time >= 10)
        {
            return _time.ToString();
        }
        else
        {
            return $"{0}{_time}";
        }
    }
}