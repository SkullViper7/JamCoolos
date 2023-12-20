using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Lightning : MonoBehaviour
{
    public GameObject cam;

    [Header("Lightning")]
    [SerializeField]
    private float _lightDuration;
    [SerializeField]
    private float _lightStrength;
    [SerializeField]
    private int _lightVibrato;
    [SerializeField]
    private float _lightRandomness;
    [SerializeField]
    private GameObject _lightning;
    [SerializeField]
    private Animator _flashAnim;
    [SerializeField]
    private AudioClip _lightningSFX;

    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public IEnumerator LightningStrikes()
    {
        yield return new WaitForSeconds(3f);
        //Get the actual best player
        GameObject playerToStrike = ScoreManager.Instance.GetBestPlayer();

        //Lightning strikes the player
        GameObject lightning = Instantiate(_lightning, playerToStrike.transform.position, Quaternion.identity);
        _flashAnim.Play("Flash");

        //Shake the camera
        cam.transform.DOShakePosition(_lightDuration, _lightStrength, _lightVibrato, _lightRandomness);

        //Makes the player fall
        PlayerStateMachine playerStateMachine = playerToStrike.GetComponent<PlayerStateMachine>();
        playerStateMachine.GetComponent<PlayerFall>().objectThatPushedMe = gameObject;
        playerStateMachine.ChangeState(playerStateMachine.fallingState);

        //Play audio
        _audioSource.PlayOneShot(_lightningSFX);

        //Makes gamepad rumble
        GamepadRumble.Instance.StartRumble(playerToStrike, _lightDuration, _lightStrength);

        //Destroy the lightning
        yield return new WaitForSeconds(0.1f);
        Destroy(lightning);

        _flashAnim.Play("Idle");
    }
}
