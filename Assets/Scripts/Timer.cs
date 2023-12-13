using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    TMP_Text time;
    public int minutes;

    private void Start()
    {
        time = GetComponent<TMP_Text>();
        int seconds = minutes * 60;
        StartCoroutine(TimeDecrease(seconds));
    }

    public IEnumerator TimeDecrease(int totalSeconds)
    {
        int count = totalSeconds;

        while (count >= 0)
        {
            int minutesLeft = count / 60;
            int secondsLeft = count % 60;

            string timeText = string.Format("{0:00}:{1:00}", minutesLeft, secondsLeft);
            time.text = timeText;

            yield return new WaitForSeconds(1);
            count--;
        }
    }
}