using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    TMP_Text text;
    public GameObject chrono;

    private void Start()
    {
        text = GetComponent<TMP_Text>();
        StartCoroutine(Count());
    }

    IEnumerator Count()
    {
        text.text = "3";
        yield return new WaitForSeconds(1);
        text.text = "2";
        yield return new WaitForSeconds(1);
        text.text = "1";
        yield return new WaitForSeconds(1);
        text.text = "GO";
        yield return new WaitForSeconds(1);
        chrono.SetActive(true);
        Destroy(gameObject);
    }
}
