using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInvincibility : MonoBehaviour
{
    private PlayerStateMachine _playerStateMachine;
    public Coroutine invincibilityCoroutine;

    public float defaultInvincibilityTime;

    private void Start()
    {
        _playerStateMachine = GetComponent<PlayerStateMachine>();
    }

    public void LaunchInvincibility()
    {
        //Start coroutine
        invincibilityCoroutine = StartCoroutine(Invincibility());
    }

    public void StopInvincibility()
    {
        //Make sure that the coroutine is clear
        if (invincibilityCoroutine != null)
        {
            StopCoroutine(invincibilityCoroutine);
            invincibilityCoroutine = null;
        }
    }

    private IEnumerator Invincibility()
    {
        //Wait until invincibility time to return to default state
        yield return new WaitForSeconds(defaultInvincibilityTime);
        _playerStateMachine.ChangeState(_playerStateMachine.defaultState);
    }
}
