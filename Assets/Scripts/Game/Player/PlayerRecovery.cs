using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRecovery : MonoBehaviour
{
    private PlayerStateMachine _playerStateMachine;
    public Coroutine recoveryCoroutine;

    public float defaultRecoveryTime;

    private void Start()
    {
        _playerStateMachine = GetComponent<PlayerStateMachine>();
    }

    public void LaunchRecovery(float recoveryTime)
    {
        //Start coroutine
        recoveryCoroutine = StartCoroutine(Recovery(recoveryTime));
    }

    public void StopRecovery()
    {
        //Make sure that the coroutine is clear
        if (recoveryCoroutine != null)
        {
            StopCoroutine(recoveryCoroutine);
            recoveryCoroutine = null;
        }
    }

    private IEnumerator Recovery(float recoveryTime)
    {
        //Wait until recovery to return to default state
        yield return new WaitForSeconds(recoveryTime);
        _playerStateMachine.ChangeState(_playerStateMachine.defaultState);
    }
}
