using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;

public class EventManager : MonoBehaviour
{
    GameObject player;
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


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //StartCoroutine(Earthquake());
        //StartCoroutine(Lightning());
        //StartCoroutine(Wind());
    }

    public IEnumerator Earthquake()
    {
        cam.GetComponent<Camera>().DOShakePosition(eqDuration, eqStrength, eqVibrato, eqRandomness);
        Gamepad.current.SetMotorSpeeds(1, 1);

        yield return new WaitForSeconds(4);

        Gamepad.current.SetMotorSpeeds(0, 0);
    }

    public IEnumerator Lightning()
    {
        yield return new WaitForSeconds(1);

        GameObject flash;
        flash = Instantiate(lightning, player.transform.position, Quaternion.identity);
        cam.GetComponent<Camera>().DOShakePosition(lDuration, lStrength, lVibrato, lRandomness);
        flashAnim.Play("Flash");
        Gamepad.current.SetMotorSpeeds(1, 1);

        yield return new WaitForSeconds(0.1f);
        Destroy(flash);

        yield return new WaitForSeconds(0.5f);
        Gamepad.current.SetMotorSpeeds(0, 0);

        player.GetComponent<PlayerControls>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        player.GetComponent<PlayerControls>().enabled = true;

    }

    public IEnumerator Wind()
    {
        yield return new WaitForSeconds(1);

        player.GetComponent<PlayerControls>().moveSpeed = 2;
        Gamepad.current.SetMotorSpeeds(0.25f, 0.25f);


        yield return new WaitForSeconds(3);

        Gamepad.current.SetMotorSpeeds(0, 0);
        player.GetComponent<PlayerControls>().moveSpeed = 5;
    }
}