using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInvincibility : MonoBehaviour
{
    private PlayerStateMachine _playerStateMachine;
    public Coroutine invincibilityCoroutine;

    [SerializeField]
    private GameObject _playerMarker;
    private Animator _playerMarkerAnimator;

    public float defaultInvincibilityTime;

    private void Start()
    {
        Chrono.Instance.EndOfTheGame += DesactivePlayerMarker;
        _playerMarkerAnimator = _playerMarker.GetComponent<Animator>();
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
            _playerMarkerAnimator.SetBool("IsInvincible", false);
            StopCoroutine(invincibilityCoroutine);
            invincibilityCoroutine = null;
        }
    }

    private IEnumerator Invincibility()
    {
        //Makes player marker flash
        _playerMarkerAnimator.SetBool("IsInvincible", true);

        //Wait until invincibility time to return to default state
        yield return new WaitForSeconds(defaultInvincibilityTime);
        _playerMarkerAnimator.SetBool("IsInvincible", false);
        _playerStateMachine.ChangeState(_playerStateMachine.defaultState);
    }

    private void DesactivePlayerMarker()
    {
        //Desactive player markers during the podium
        _playerMarker.SetActive(false);
    }
}
