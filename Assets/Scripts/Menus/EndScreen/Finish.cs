using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    public int duration;
    public int strenght;
    public int vibrato;
    public int randomness;

    private void Start()
    {
        gameObject.transform.DOShakePosition(duration, strenght, vibrato, randomness, false, false);
        Invoke("Delete", duration);
    }

    void Delete()
    {
        Destroy(gameObject);
    }
}