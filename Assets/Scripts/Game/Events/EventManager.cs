using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;

public class EventManager : MonoBehaviour
{
    GameObject[] players;

    public GameObject cam;

    [Header("Earthquake")]
    public float eqDuration;
    public float eqStrength;
    public int eqVibrato;
    public float eqRandomness;

    [Header("Lightning")]
    public float lDuration;
    public float lStrength;
    public int lVibrato;
    public float lRandomness;
    public GameObject lightning;
    public Animator flashAnim;
    public AudioClip lightningSFX;

    AudioSource audioSource;
    bool hasMetal;

    bool isLightningRunnig;
    bool isEarthquakeRunning;
    bool isWindRunning;

    private List<Gamepad> gamepads = new List<Gamepad>();
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        players = GameObject.FindGameObjectsWithTag("Player");

        int randomLightningTime = Random.Range(10, 15);
        int randomEarthquakeTime = Random.Range(10, 15);
        int randomWindTime = Random.Range(10, 15);

        StartCoroutine(Earthquake(randomEarthquakeTime));
        StartCoroutine(Lightning(randomLightningTime));
        StartCoroutine(Wind(randomWindTime));

        for (int i = 0; i < players.Length; i++)
        {
            var playerInput = players[i].GetComponent<PlayerDevice>().playerInput;
            if (playerInput != null && playerInput.user.pairedDevices.Count > 0)
            {
                var gamepad = (Gamepad)playerInput.user.pairedDevices[0];
                gamepads.Add(gamepad);
            }
        }
    }

    public IEnumerator Earthquake(int randomTime)
    {
        isEarthquakeRunning = false;

        yield return new WaitForSeconds(randomTime);

        if (isLightningRunnig || isWindRunning)
        {
            yield return new WaitForSeconds(10);
        }

        isEarthquakeRunning = true;

        for (int i = 0; i < gamepads.Count; i++)
        {
            cam.GetComponent<Camera>().DOShakePosition(eqDuration, eqStrength, eqVibrato, eqRandomness);
            gamepads[i].SetMotorSpeeds(1, 1);
            players[i].GetComponent<CollectObjects>().DropObject();

            yield return new WaitForSeconds(4);

            gamepads[i].SetMotorSpeeds(0, 0);
        }

        int newRandomTime = Random.Range(10, 15);

        StartCoroutine(Earthquake(newRandomTime));
    }

    public IEnumerator Lightning(int randomTime)
    {
        int randomStrike = Random.Range(0, players.Length);

        isLightningRunnig = false;

        yield return new WaitForSeconds(randomTime);

        if (isEarthquakeRunning || isWindRunning)
        {
            yield return new WaitForSeconds(10);
        }

        isLightningRunnig = true;

        GameObject flash;
        flash = Instantiate(lightning, players[randomStrike].transform.position, Quaternion.identity);
        flashAnim.Play("Flash");

        cam.GetComponent<Camera>().DOShakePosition(lDuration, lStrength, lVibrato, lRandomness);

        audioSource.PlayOneShot(lightningSFX);

        StartCoroutine(GamepadRumble.Instance.Rumble(players[randomStrike], 0.5f, 1));

        Movements movements = players[randomStrike].GetComponent<Movements>();
        float initalSpeed = movements.moveSpeed;
        movements.moveSpeed = 0;

        players[randomStrike].GetComponent<CollectObjects>().DropObject();

        yield return new WaitForSeconds(0.1f);
        Destroy(flash);

        yield return new WaitForSeconds(1);

        players[randomStrike].GetComponent<Movements>().moveSpeed = initalSpeed;
        flashAnim.Play("Idle");

        int newRandomTime = Random.Range(10, 15);

        if (!isEarthquakeRunning && !isWindRunning)
        {
            StartCoroutine(Lightning(newRandomTime));

            yield return new WaitForSeconds(5);

            StartCoroutine(Lightning(newRandomTime));
        }
    }

    public IEnumerator Wind(int randomTime)
    {
        isWindRunning = false;

        yield return new WaitForSeconds(randomTime);

        if (isLightningRunnig || isEarthquakeRunning)
        {
            yield return new WaitForSeconds(10);
        }

        isWindRunning = true;

        for (int i = 0; i < players.Length; i++)
        {
            players[i].GetComponent<Movements>().moveSpeed /= 2;
            StartCoroutine(GamepadRumble.Instance.Rumble(players[i], 0.5f, 0.5f));
        }

        yield return new WaitForSeconds(3);

        for (int i = 0; i < players.Length; i++)
        {
            players[i].GetComponent<Movements>().moveSpeed *= 2;
        }

        int newRandomTime = Random.Range(10, 15);

        if (!isEarthquakeRunning && !isLightningRunnig)
        {
            StartCoroutine(Wind(newRandomTime));

            yield return new WaitForSeconds(5);

            StartCoroutine(Wind(newRandomTime));
        }
    }
}