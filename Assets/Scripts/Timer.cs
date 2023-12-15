using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    TMP_Text time;
    public float minutes;

    [Space]
    public GameObject EndScreen;
    public GameObject gameUI;
    public AudioSource gameMusic;
    public AudioSource endMusic;

    private void Start()
    {
        time = GetComponent<TMP_Text>();
        float seconds = minutes * 60f;
        StartCoroutine(TimeDecrease(seconds));
    }

    public IEnumerator TimeDecrease(float totalSeconds)
    {
        float count = totalSeconds;

        while (count >= 0)
        {
            float minutesLeft = count / 60;
            float secondsLeft = count % 60;

            string timeText = string.Format("{0:00}:{1:00}", minutesLeft, secondsLeft);
            time.text = timeText;

            yield return new WaitForSeconds(1);
            count--;

            if (count == 0)
            {
                EndScreen.SetActive(true);
                gameUI.SetActive(false);
                gameMusic.Stop();
                endMusic.Play();
            }
        }
    }
}