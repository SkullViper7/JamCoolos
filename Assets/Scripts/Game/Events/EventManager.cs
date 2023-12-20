using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;

public class EventManager : MonoBehaviour
{
//    [Header("Earthquake")]
//    [SerializeField]
//    private float _earthquakeDuration;
//    [SerializeField]
//    private float _earthquakeStrength;
//    [SerializeField]
//    private int _earthquakeVibrato;
//    [SerializeField]
//    private float _earthquakeRandomness;
//    [SerializeField]
//    private AudioClip _earthquakeSFX;

//    [Header("Wind")]
//    [SerializeField]
//    private AudioClip _windSFX;

//    private void Start()
//    {
//        StartCoroutine(PlayEvent());
//    }

//    IEnumerator PlayEvent()
//    {
//        int randomEvent = Random.Range(1, 4);
//        int randomTime = Random.Range(10, 15);

//        yield return new WaitForSeconds(randomTime);

//        switch (randomEvent)
//        {
//            case 1 : StartCoroutine(Earthquake());
//                break;
//            case 3: StartCoroutine(Wind());
//                break;
//        }

//        StartCoroutine(PlayEvent());
//    }

//    public IEnumerator Earthquake()
//    {
//        Debug.Log("Earthquake");
//        cam.GetComponent<Camera>().DOShakePosition(_earthquakeDuration, _earthquakeStrength, _earthquakeVibrato, _earthquakeRandomness, false);
//        for (int i = 0; i < _manager.gamepads.Count; i++)
//        {
//            GamepadRumble.Instance.StartRumble(_manager.players[i], _earthquakeDuration, 1);
//            _audioSource.PlayOneShot(_earthquakeSFX);

//            PlayerStateMachine stateMachine = _manager.players[i].GetComponent<PlayerStateMachine>();
//            stateMachine.GetComponent<PlayerFall>().objectThatPushedMe = gameObject;
//            stateMachine.ChangeState(stateMachine.fallingState);
//        }

//        yield return new WaitForSeconds(_earthquakeDuration);

//        for (int i = 0; i < _manager.gamepads.Count; i++)
//        {
//            _manager.players[i].GetComponent<Movements>().actualSpeed = 300;
//        }
//    }

//    public IEnumerator Wind()
//    {
//        Debug.Log("Wind");
//        for (int i = 0; i < _manager.players.Count; i++)
//        {
//            _manager.players[i].GetComponent<Movements>().actualSpeed /= 2;
//            GamepadRumble.Instance.StartRumble(_manager.players[i], 3, 0.5f);
//            _audioSource.volume = 0.5f;
//            _audioSource.PlayOneShot(_windSFX);
//        }

//        yield return new WaitForSeconds(3);

//        for (int i = 0; i < _manager.players.Count; i++)
//        {
//            _manager.players[i].GetComponent<Movements>().actualSpeed *= 2;
//            _audioSource.volume = 1;
//        }

//        int newRandomTime = Random.Range(10, 15);
//    }
}