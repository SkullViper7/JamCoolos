using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;

public class Earthquake : MonoBehaviour
{
    [SerializeField]
    private GameObject _camera;
    private AudioSource _audioSource;
    private GameManager _gameManager;
    private EventManager _eventManager;

    [Header("Earthquake")]
    [SerializeField]
    private float _earthquakeDuration;
    [SerializeField]
    private float _earthquakeStrength;
    [SerializeField]
    private int _earthquakeVibrato;
    [SerializeField]
    private float _earthquakeRandomness;
    [SerializeField]
    private AudioClip _earthquakeSFX;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _gameManager =  GameManager.Instance;
        _eventManager = GetComponent<EventManager>();
    }

    public void DoEarthquake()
    {
        StartCoroutine(EarthQuakes());
    }

    public IEnumerator EarthQuakes()
    {
        //Shake the camera
        _camera.GetComponent<CinemachineConfiner>().enabled = false;
        _camera.transform.DOShakePosition(_earthquakeDuration, _earthquakeStrength, _earthquakeVibrato, _earthquakeRandomness);

        //Makes all gamepads rumble and players fall forward
        for (int i = 0; i < _gameManager.players.Count; i++)
        {
            GamepadRumble.Instance.StartRumble(_gameManager.players[i], _earthquakeDuration, _earthquakeStrength);

            PlayerStateMachine playerStateMachine = _gameManager.players[i].GetComponent<PlayerStateMachine>();
            gameObject.transform.rotation = Quaternion.Euler(0f, playerStateMachine.transform.rotation.y, 0f);
            playerStateMachine.GetComponent<PlayerFall>().objectThatPushedMe = gameObject;
            playerStateMachine.ChangeState(playerStateMachine.fallingState);
        }
        
        //Play audio
        _audioSource.PlayOneShot(_earthquakeSFX);

        yield return new WaitForSeconds(_earthquakeDuration);

        _camera.GetComponent<CinemachineConfiner>().enabled = true;

        gameObject.transform.rotation = Quaternion.identity;
        _eventManager.isThereAnEventInProgress = false;
    }
}
