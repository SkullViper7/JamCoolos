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

    private List<Gamepad> gamepads = new List<Gamepad>();
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        players = GameObject.FindGameObjectsWithTag("Player");
        //StartCoroutine(Earthquake());
        //StartCoroutine(Lightning());
        //StartCoroutine(Wind());

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

    public IEnumerator Earthquake()
    {
        for (int i = 0; i < gamepads.Count; i++)
        {
            cam.GetComponent<Camera>().DOShakePosition(eqDuration, eqStrength, eqVibrato, eqRandomness);
            gamepads[i].SetMotorSpeeds(1, 1);

            yield return new WaitForSeconds(4);

            gamepads[i].SetMotorSpeeds(0, 0);
        }
    }

    public IEnumerator Lightning()
    {
        int randomStrike = Random.Range(0, players.Length);
        int randomTime = Random.Range(10, 20);

        yield return new WaitForSeconds(randomTime);

        GameObject flash;
        flash = Instantiate(lightning, players[randomStrike].transform.position, Quaternion.identity);
        cam.GetComponent<Camera>().DOShakePosition(lDuration, lStrength, lVibrato, lRandomness);
        flashAnim.Play("Flash");
        audioSource.PlayOneShot(lightningSFX);
        gamepads[randomStrike].SetMotorSpeeds(1, 1);
        Movements movements = GetComponent<Movements>();
        float initalSpeed = movements.moveSpeed;
        movements.moveSpeed = 0;

        yield return new WaitForSeconds(0.1f);
        Destroy(flash);

        yield return new WaitForSeconds(0.4f);
        gamepads[randomStrike].SetMotorSpeeds(0, 0);

        yield return new WaitForSeconds(1);
        movements.moveSpeed = initalSpeed;
        flashAnim.Play("Idle");

        StartCoroutine(Lightning());
    }

    public IEnumerator Wind()
    {
        yield return new WaitForSeconds(1);

        for (int i = 0; i < players.Length; i++)
        {
            players[i].GetComponent<Movements>().moveSpeed /= 2;
            gamepads[i].SetMotorSpeeds(1, 1);
        }

        yield return new WaitForSeconds(3);

        for (int i = 0; i < players.Length; i++)
        {
            players[i].GetComponent<Movements>().moveSpeed *= 2;
            gamepads[i].SetMotorSpeeds(0, 0);
        }
    }
}