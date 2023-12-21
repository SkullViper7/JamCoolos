using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Wind : MonoBehaviour
{
    [SerializeField]
    private GameObject _camera;
    private AudioSource _audioSource;
    private GameManager _gameManager;
    private EventManager _eventManager;

    [Header("Wind")]
    [SerializeField]
    private float _windDuration;
    [SerializeField]
    private float _windStrength;
    [SerializeField]
    private AudioClip _windSFX;
    [SerializeField]
    private ParticleSystem _windVFX;

    private bool _windIsBlowing;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _gameManager = GameManager.Instance;
        _eventManager = GetComponent<EventManager>();
    }

    public void DoWindBlow()
    {
        StartCoroutine(WindBlows());
    }

    private IEnumerator WindBlows()
    {
        _windIsBlowing = true;
        //Make all gamepads rumble
        for (int i = 0; i < _gameManager.players.Count; i++)
        {
            GamepadRumble.Instance.StartRumble(_gameManager.players[i], _windDuration, _windStrength);
        }
        
        //Play audio
        _audioSource.volume = 0.5f;
        _audioSource.PlayOneShot(_windSFX);
        _windVFX.Play();

        //Wait during the wind
        yield return new WaitForSeconds(_windDuration);

        _windIsBlowing = false;
        _windVFX.Stop();

        //Reset movespeeds
        for (int i = 0; i < _gameManager.players.Count; i++)
        {
            Movements playerMovements = _gameManager.players[i].GetComponent<Movements>();
            PlayerStateMachine playerStateMachine = _gameManager.players[i].GetComponent<PlayerStateMachine>();

            //If player is holding an object
            if (playerStateMachine.currentState == playerStateMachine.holdingState)
            {
                playerMovements.actualSpeed = playerMovements.defaultMoveSpeed * playerStateMachine.holdingState.SpeedCoefficient();
            }
            //If not
            else
            {
                playerMovements.actualSpeed = playerMovements.defaultMoveSpeed;
            }
        }

        _audioSource.volume = 1;

        _eventManager.isThereAnEventInProgress = false;
    }

    private void FixedUpdate()
    {
        //Divide player speed by 2
        if (_windIsBlowing)
        {
            for (int i = 0; i < _gameManager.players.Count; i++)
            {
                Movements playerMovements = _gameManager.players[i].GetComponent<Movements>();
                playerMovements.actualSpeed = playerMovements.defaultMoveSpeed / 2;
            }
        }
    }
}
