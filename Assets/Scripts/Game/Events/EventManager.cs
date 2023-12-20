using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;

public class EventManager : MonoBehaviour
{
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

    GameManager manager;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        manager = GameManager.Instance;
        
        StartCoroutine(PlayEvent());
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
        for (int i = 0; i < manager.gamepads.Count; i++)
        {
            GamepadRumble.Instance.StartRumble(manager.players[i], eqDuration, 1);
            audioSource.PlayOneShot(earthquakeSFX);

            PlayerStateMachine stateMachine = manager.players[i].GetComponent<PlayerStateMachine>();
            stateMachine.GetComponent<PlayerFall>().objectThatPushedMe = gameObject;
            stateMachine.ChangeState(stateMachine.fallingState);
        }

        yield return new WaitForSeconds(eqDuration);

        for (int i = 0; i < manager.gamepads.Count; i++)
        {
            manager.players[i].GetComponent<Movements>().actualSpeed = 300;
        }
    }

    public IEnumerator Lightning()
    {
        int randomStrike = Random.Range(0, manager.players.Count);

        GameObject flash;
        flash = Instantiate(lightning, manager.players[randomStrike].transform.position, Quaternion.identity);
        flashAnim.Play("Flash");

        cam.GetComponent<Camera>().DOShakePosition(lDuration, lStrength, lVibrato, lRandomness);

        PlayerStateMachine stateMachine = manager.players[randomStrike].GetComponent<PlayerStateMachine>();
        stateMachine.GetComponent<PlayerFall>().objectThatPushedMe = gameObject;
        stateMachine.ChangeState(stateMachine.fallingState);


        audioSource.PlayOneShot(lightningSFX);

        GamepadRumble.Instance.StartRumble(manager.players[randomStrike], 0.5f, 1);

        Movements movements = manager.players[randomStrike].GetComponent<Movements>();
        float initalSpeed = movements.defaultMoveSpeed;
        movements.actualSpeed = 0;

        if (manager.players[randomStrike].GetComponent<PlayerStateMachine>().currentState == manager.players[randomStrike].GetComponent<PlayerStateMachine>().holdingState)
        {
            manager.players[randomStrike].GetComponent<CollectObjects>().DropObject();
        }

        yield return new WaitForSeconds(0.1f);
        Destroy(flash);

        yield return new WaitForSeconds(lDuration);

        manager.players[randomStrike].GetComponent<Movements>().actualSpeed = initalSpeed;
        flashAnim.Play("Idle");
    }

    public IEnumerator Wind()
    {
        Debug.Log("Wind");
        for (int i = 0; i < manager.players.Count; i++)
        {
            manager.players[i].GetComponent<Movements>().actualSpeed /= 2;
            GamepadRumble.Instance.StartRumble(manager.players[i], 3, 0.5f);
            audioSource.volume = 0.5f;
            audioSource.PlayOneShot(windSFX);
        }

        yield return new WaitForSeconds(3);

        for (int i = 0; i < manager.players.Count; i++)
        {
            manager.players[i].GetComponent<Movements>().actualSpeed *= 2;
            audioSource.volume = 1;
        }

        int newRandomTime = Random.Range(10, 15);
    }
}