using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingState : IPlayerState
{
    private PlayerFall _playerFall;
    private CollectObjects _collectObjects;

    public void OnEnter(PlayerStateMachine stateMachine)
    {
        _playerFall = stateMachine.playerFall;
        _collectObjects = stateMachine.collectObjects;

        if (_playerFall.objectThatPushedMe.CompareTag("Player"))
        {
            //Player falls
            _playerFall.Fall(_playerFall.objectThatPushedMe.transform.forward);
        }
        else
        {
            int randomAngle = Random.Range(0, 2);

            if (randomAngle == 0)
            {
                _playerFall.Fall(_playerFall.objectThatPushedMe.transform.forward);
            }
            if (randomAngle == 1)
            {
                _playerFall.Fall(-_playerFall.objectThatPushedMe.transform.forward);
            }
        }

        //If player held an object, he drops it
        if (_collectObjects.objectThatIsHeld != null)
        {
            _playerFall.DropObjectWhenPlayerFalls(_collectObjects.objectThatIsHeld);
        }
    }

    public void OnExit(PlayerStateMachine stateMachine)
    {

    }
}
