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
        for (int i = 0; i < gamepads.Count; i++)
        {
            cam.GetComponent<Camera>().DOShakePosition(eqDuration, eqStrength, eqVibrato, eqRandomness, false);
            StartCoroutine(GamepadRumble.Instance.Rumble(players[i], 4, 1));
            if (players[i].GetComponent<StateMachine>().currentState == players[i].GetComponent<StateMachine>().holdingState)
            {
                players[i].GetComponent<CollectObjects>().DropObject();
            }
        }

        yield return null;
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
        float initalSpeed = movements.moveSpeed;
        movements.moveSpeed = 0;

        if (players[randomStrike].GetComponent<StateMachine>().currentState == players[randomStrike].GetComponent<StateMachine>().holdingState)
        {
            players[randomStrike].GetComponent<CollectObjects>().DropObject();
        }

        yield return new WaitForSeconds(0.1f);
        Destroy(flash);

        yield return new WaitForSeconds(1);

        players[randomStrike].GetComponent<Movements>().moveSpeed = initalSpeed;
        flashAnim.Play("Idle");
    }

    public IEnumerator Wind()
    {
        Debug.Log("Wind");
        for (int i = 0; i < players.Length; i++)
        {
            players[i].GetComponent<Movements>().moveSpeed /= 2;
            StartCoroutine(GamepadRumble.Instance.Rumble(players[i], 3, 0.5f));
        }

        yield return new WaitForSeconds(3);

        for (int i = 0; i < players.Length; i++)
        {
            players[i].GetComponent<Movements>().moveSpeed *= 2;
        }

        int newRandomTime = Random.Range(10, 15);
    }
}