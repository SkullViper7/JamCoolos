using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingState : IPlayerState
{
    private PlayerFall playerFall;
    private CollectObjects collectObjects;

    public void OnEnter(PlayerStateMachine _stateMachine)
    {
        playerFall = _stateMachine.playerFall;
        collectObjects = _stateMachine.collectObjects;

        //Player falls
        playerFall.Fall(playerFall.playerThatPushedMe.transform.forward);

        //If player held an object, he drops it
        if (collectObjects.objectThatIsHeld != null)
        {
            playerFall.DropObjectWhenPlayerFalls(collectObjects.objectThatIsHeld);
        }
    }

    public void UpdateState(PlayerStateMachine _stateMachine)
    {

    }

    public void OnExit(PlayerStateMachine _stateMachine)
    {

    }
}
