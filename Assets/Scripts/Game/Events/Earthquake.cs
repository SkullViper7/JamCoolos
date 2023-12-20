using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
        _camera.transform.DOShakePosition(_earthquakeDuration, _earthquakeStrength, _earthquakeVibrato, _earthquakeRandomness);

        //Makes all gamepads rumble and players fall forward
        for (int i = 0; i < _gameManager.players.Count; i++)
        {
            GamepadRumble.Instance.StartRumble(_gameManager.players[i], _earthquakeDuration, _earthquakeStrength);

            PlayerStateMachine playerStateMachine = _gameManager.players[i].GetComponent<PlayerStateMachine>();
            transform.rotation  = playerStateMachine.transform.rotation;
            playerStateMachine.GetComponent<PlayerFall>().objectThatPushedMe = gameObject;
            playerStateMachine.ChangeState(playerStateMachine.fallingState);
        }
        
        //Play audio
        _audioSource.PlayOneShot(_earthquakeSFX);

        yield return new WaitForSeconds(_earthquakeDuration);

        _eventManager.isThereAnEventInProgress = false;
    }
}
