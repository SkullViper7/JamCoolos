using System.Collections;
using UnityEngine;
using TMPro;
using System;
using Cinemachine;

public class Chrono : MonoBehaviour
{
    [SerializeField]
    private float time;

    [SerializeField]
    private TMP_Text _minutes;
    [SerializeField]
    private TMP_Text _seconds;
    [SerializeField]
    private Coroutine _decrementTimer;

    private int _nbrOfMinutes;
    private int _nbrOfSeconds;

    [SerializeField]
    private GameObject _finish;
    [SerializeField]
    private GameObject _gameUI;
    [SerializeField]
    private AudioSource _gameMusic;
    [SerializeField]
    private AudioSource _endMusic;
    [SerializeField] 
    private AudioSource _buzzer;
    [SerializeField]
    private GameObject _endScreen;

    [SerializeField]
    private GameObject _cam;
    [SerializeField] 
    private GameObject _camTarget;
    [SerializeField]
    private Podium _podiumScript;
    [SerializeField]
    private Animator fadeAnim;

    //Singleton
    private static Chrono _instance = null;
    public static Chrono Instance => _instance;
    //

    // Observer
    public delegate void ProgressDelegate();

    public event ProgressDelegate NewSecond;
    public event ProgressDelegate TiersOfTheGame;
    public event ProgressDelegate HalfOfTheGame;
    public event ProgressDelegate LastQuarterOfTheGame;
    public event ProgressDelegate EndOfTheGame;
    //

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
        ConvertTimeIntoChrono(time);

        _minutes.SetText(ConvertToString(_nbrOfMinutes));
        _seconds.SetText(ConvertToString(_nbrOfSeconds));

        _decrementTimer = StartCoroutine(DecrementChrono());
    }

    private void ConvertTimeIntoChrono(float _time)
    {
        // Convert seconds into minutes and seconds
        _nbrOfMinutes = Mathf.FloorToInt(_time / 60f);
        _nbrOfSeconds = (int)(_time - (Mathf.FloorToInt(_time / 60f) * 60f));
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
        if (_nbrOfSeconds - 1 == -1 && _nbrOfMinutes != 0)
        {
            _nbrOfSeconds = 59;
            _nbrOfMinutes -= 1;

            _minutes.SetText(ConvertToString(_nbrOfMinutes));
            _seconds.SetText(ConvertToString(_nbrOfSeconds));
            _decrementTimer = StartCoroutine(DecrementChrono());
        }
        else if (_nbrOfSeconds - 1 == -1 && _nbrOfMinutes == 0)
        {
            StopTimer();
        }
        else
        {
            _nbrOfSeconds -= 1;
            _seconds.SetText(ConvertToString(_nbrOfSeconds));
            _decrementTimer = StartCoroutine(DecrementChrono());
        }

        //Invoke this event at each second
        NewSecond?.Invoke();
    }

    private void CheckGameProgress()
    {
        //Check if it's the tiers of the game
        if (ConvertChronoIntoTime(_nbrOfMinutes, _nbrOfSeconds) == Math.Floor(time / 3) * 2)
        {
            TiersOfTheGame?.Invoke();
        }
        //Check if it's the half of the game
        else if (ConvertChronoIntoTime(_nbrOfMinutes, _nbrOfSeconds) == Math.Floor(time / 2))
        {
            HalfOfTheGame?.Invoke();
        }
        //Check if it's the last quarter of the game
        else if (ConvertChronoIntoTime(_nbrOfMinutes, _nbrOfSeconds) == Math.Floor(time / 4))
        {
            LastQuarterOfTheGame?.Invoke();
        }
    }

    public void StopTimer()
    {
        // Stop the timer
        StopCoroutine(_decrementTimer);
        _finish.SetActive(true);
        _gameUI.SetActive(false);
        _gameMusic.Stop();
        _buzzer.Play();
        _endMusic.Play();
        GameManager.Instance.isGameOver = true;
        EndOfTheGame?.Invoke();
        Invoke("ShowPodium", 2);
    }

    void ShowPodium()
    {
        _podiumScript.MoveToPodium();
        _endScreen.SetActive(true);
        fadeAnim.Play("Fade");
        Invoke("MoveCam", 0.6f);
    }

    void MoveCam()
    {
        _cam.GetComponent<MultiTargetCam>().enabled = false;
        _cam.GetComponent<CinemachineConfiner>().enabled = false;
        _cam.transform.position = _camTarget.transform.position;
        _cam.transform.rotation = _camTarget.transform.rotation;
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