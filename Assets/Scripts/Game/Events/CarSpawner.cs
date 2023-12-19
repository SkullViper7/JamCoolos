using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public GameObject carPrefab;

    [Space]
    public Transform rightSpawn;
    public Transform leftSpawn;

    [Space]
    public Transform rightTarget;
    public Transform leftTarget;

    [Space]
    public int carSpeed;

    private void Start()
    {
        StartCoroutine(LeftCarSpawn());
        StartCoroutine(RightCarSpawn());
    }

    IEnumerator LeftCarSpawn()
    {
        int randomTime = Random.Range(3, 7);

        yield return new WaitForSeconds(randomTime);

        LeftCarMove();
        StartCoroutine(LeftCarSpawn());
    }

    IEnumerator RightCarSpawn()
    {
        int randomTime = Random.Range(3, 7);

        yield return new WaitForSeconds(randomTime);
        RightCarMove();
        StartCoroutine(RightCarSpawn());
    }

    void LeftCarMove()
    {
        GameObject carLeft;
        carLeft = GameObject.Instantiate(carPrefab, leftSpawn.position, Quaternion.identity);

        carLeft.transform.DOMove(leftTarget.position, carSpeed);

        StartCoroutine(DestroyCar(carLeft, leftTarget, leftSpawn));
    }

    void RightCarMove()
    {
        GameObject carRight;
        carRight = GameObject.Instantiate(carPrefab, rightSpawn.position, Quaternion.identity);

        carRight.transform.DOMove(rightTarget.position, carSpeed);

        StartCoroutine(DestroyCar(carRight, rightTarget, rightSpawn));
    }

    IEnumerator DestroyCar(GameObject car, Transform target, Transform spawn)
    {
        yield return new WaitForSeconds(4);

        Destroy(car);
    }
}