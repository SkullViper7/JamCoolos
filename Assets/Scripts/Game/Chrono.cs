using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    private void Start()
    {
        // Convert seconds into minutes and seconds
        nbrOfMinutes = Mathf.FloorToInt(time / 60f);
        nbrOfSeconds = (int)(time - (Mathf.FloorToInt(time / 60f) * 60f));

        minutes.SetText(ConvertToString(nbrOfMinutes));
        seconds.SetText(ConvertToString(nbrOfSeconds));

        decrementTimer = StartCoroutine(DecrementChrono());
    }

    private IEnumerator DecrementChrono()
    {
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
    }

    public void StopTimer()
    {
        // Stop the timer
        StopCoroutine(decrementTimer);
        EndScreen.SetActive(true);
        gameUI.SetActive(false);
        gameMusic.Stop();
        endMusic.Play();
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