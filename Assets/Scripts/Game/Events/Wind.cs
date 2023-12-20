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
        //Divides player speed by 2 and make all gamepads rumble
        for (int i = 0; i < _gameManager.players.Count; i++)
        {
            _gameManager.players[i].GetComponent<Movements>().actualSpeed /= 2;
            GamepadRumble.Instance.StartRumble(_gameManager.players[i], _windDuration, _windStrength);
        }
        
        //Play audio
        _audioSource.volume = 0.5f;
        _audioSource.PlayOneShot(_windSFX);

        //Wait during the wind
        yield return new WaitForSeconds(_windDuration);

        //Reset movespeeds
        for (int i = 0; i < _gameManager.players.Count; i++)
        {
            Movements playerMovements = _gameManager.players[i].GetComponent<Movements>();
            playerMovements.actualSpeed = playerMovements.defaultMoveSpeed;
        }

        _audioSource.volume = 1;

        _eventManager.isThereAnEventInProgress = false;
    }
}
