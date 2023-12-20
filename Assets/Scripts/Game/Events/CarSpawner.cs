using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public List<GameObject> carPrefab;

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
        int randomCar = Random.Range(0, carPrefab.Count);
        GameObject carLeft;
        carLeft = GameObject.Instantiate(carPrefab[randomCar], leftSpawn.position, Quaternion.Euler(0, 180, 0));

        carLeft.transform.DOMove(leftTarget.position, carSpeed);

        StartCoroutine(DestroyCar(carLeft));
    }

    void RightCarMove()
    {
        int randomCar = Random.Range(0, carPrefab.Count);
        GameObject carRight;
        carRight = GameObject.Instantiate(carPrefab[randomCar], rightSpawn.position, Quaternion.identity);

        carRight.transform.DOMove(rightTarget.position, carSpeed);

        StartCoroutine(DestroyCar(carRight));
    }

    IEnumerator DestroyCar(GameObject car)
    {
        yield return new WaitForSeconds(carSpeed);

        Destroy(car);
    }
}