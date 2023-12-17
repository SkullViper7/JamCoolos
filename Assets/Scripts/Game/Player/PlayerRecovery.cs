using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRecovery : MonoBehaviour
{
    private PlayerStateMachine playerStateMachine;
    public Coroutine recoveryCoroutine;

    public float defaultRecoveryTime;

    private void Start()
    {
        playerStateMachine = GetComponent<PlayerStateMachine>();
    }

    public void LaunchRecovery(float _recoveryTime)
    {
        //Start coroutine
        recoveryCoroutine = StartCoroutine(Recovery(_recoveryTime));
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

    private IEnumerator Recovery(float _recoveryTime)
    {
        //Wait until recovery to return to default state
        yield return new WaitForSeconds(_recoveryTime);
        playerStateMachine.ChangeState(playerStateMachine.defaultState);
    }
}
