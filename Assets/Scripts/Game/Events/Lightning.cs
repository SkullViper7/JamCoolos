using Cinemachine;
using DG.Tweening;
using System.Collections;
using UnityEditor;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    [SerializeField]
    private GameObject _camera;
    private AudioSource _audioSource;
    private EventManager _eventManager;

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

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _eventManager = GetComponent<EventManager>();
    }

    public void DoLightningStrike()
    {
        StartCoroutine(LightningStrikes());
    }

    private IEnumerator LightningStrikes()
    {
        //Get the actual best player
        GameObject playerToStrike = ScoreManager.Instance.GetBestPlayer();

        //Lightning strikes the player
        GameObject lightning = Instantiate(_lightning, playerToStrike.transform.position, Quaternion.identity);
        _flashAnim.Play("Flash");

        //Shake the camera
        _camera.GetComponent<CinemachineConfiner>().enabled = false;
        _camera.transform.DOShakePosition(_lightDuration, _lightStrength, _lightVibrato, _lightRandomness);

        //Makes the player fall forward
        PlayerStateMachine playerStateMachine = playerToStrike.GetComponent<PlayerStateMachine>();
        gameObject.transform.rotation = Quaternion.Euler(0f, playerToStrike.transform.rotation.y, 0f);
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

        gameObject.transform.rotation = Quaternion.identity;

        _camera.GetComponent<CinemachineConfiner>().enabled = true;

        yield return new WaitForSeconds(1);

        _eventManager.isThereAnEventInProgress = false;
    }
}
