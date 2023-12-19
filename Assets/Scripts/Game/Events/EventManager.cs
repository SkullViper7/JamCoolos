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
    public AudioClip earthquakeSFX;

    [Header("Lightning")]
    public float lDuration;
    public float lStrength;
    public int lVibrato;
    public float lRandomness;
    public GameObject lightning;
    public Animator flashAnim;
    public AudioClip lightningSFX;

    [Space]
    public AudioClip windSFX;

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

        StartCoroutine(PlayEvent());

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

    IEnumerator PlayEvent()
    {
        int randomEvent = Random.Range(1, 4);
        int randomTime = Random.Range(10, 15);

        yield return new WaitForSeconds(randomTime);

        switch (randomEvent)
        {
            case 1 : StartCoroutine(Earthquake());
                break;
            case 2: StartCoroutine(Lightning());
                break;
            case 3: StartCoroutine(Wind());
                break;
        }

        StartCoroutine(PlayEvent());
    }

    public IEnumerator Earthquake()
    {
        Debug.Log("Earthquake");
        cam.GetComponent<Camera>().DOShakePosition(eqDuration, eqStrength, eqVibrato, eqRandomness, false);
        for (int i = 0; i < gamepads.Count; i++)
        {
            StartCoroutine(GamepadRumble.Instance.Rumble(players[i], 4, 1));
            audioSource.PlayOneShot(earthquakeSFX);
            Movements movements = players[i].GetComponent<Movements>();
            movements.actualSpeed = 0;
            if (players[i].GetComponent<PlayerStateMachine>().currentState == players[i].GetComponent<PlayerStateMachine>().holdingState)
            {
                players[i].GetComponent<CollectObjects>().DropObject();
            }
        }

        yield return new WaitForSeconds(eqDuration);

        for (int i = 0; i < gamepads.Count; i++)
        {
            players[i].GetComponent<Movements>().actualSpeed = 300;
        }
    }

    public IEnumerator Lightning()
    {
        int randomStrike = Random.Range(0, players.Length);

        GameObject flash;
        flash = Instantiate(lightning, players[randomStrike].transform.position, Quaternion.identity);
        flashAnim.Play("Flash");

        cam.GetComponent<Camera>().DOShakePosition(lDuration, lStrength, lVibrato, lRandomness);

        audioSource.PlayOneShot(lightningSFX);

        StartCoroutine(GamepadRumble.Instance.Rumble(players[randomStrike], 0.5f, 1));

        Movements movements = players[randomStrike].GetComponent<Movements>();
        float initalSpeed = movements.defaultMoveSpeed;
        movements.actualSpeed = 0;

        if (players[randomStrike].GetComponent<PlayerStateMachine>().currentState == players[randomStrike].GetComponent<PlayerStateMachine>().holdingState)
        {
            players[randomStrike].GetComponent<CollectObjects>().DropObject();
        }

        yield return new WaitForSeconds(0.1f);
        Destroy(flash);

        yield return new WaitForSeconds(lDuration);

        players[randomStrike].GetComponent<Movements>().actualSpeed = initalSpeed;
        flashAnim.Play("Idle");
    }

    public IEnumerator Wind()
    {
        Debug.Log("Wind");
        for (int i = 0; i < players.Length; i++)
        {
            players[i].GetComponent<Movements>().actualSpeed /= 2;
            StartCoroutine(GamepadRumble.Instance.Rumble(players[i], 3, 0.5f));
            audioSource.volume = 0.5f;
            audioSource.PlayOneShot(windSFX);
        }

        yield return new WaitForSeconds(3);

        for (int i = 0; i < players.Length; i++)
        {
            players[i].GetComponent<Movements>().actualSpeed *= 2;
            audioSource.volume = 1;
        }

        int newRandomTime = Random.Range(10, 15);
    }
}