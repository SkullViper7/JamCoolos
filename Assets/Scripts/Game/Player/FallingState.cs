using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingState : IState
{
    private PlayerFall playerFall;
    private CollectObjects collectObjects;

    public void OnEnter(StateMachine _stateMachine)
    {
        Debug.Log("FallingState");

        playerFall = _stateMachine.playerFall;
        collectObjects = _stateMachine.collectObjects;

        //Player falls
        playerFall.Fall(playerFall.playerThatHavePushingMe.transform.forward);

        //If player held an object it drops it
        if (collectObjects.objectThatIsHeld != null)
        {
            playerFall.DropObjectWhenPlayerFalls(collectObjects.objectThatIsHeld);
        }
    }

    public void UpdateState(StateMachine _stateMachine)
    {

    }

    public void OnExit(StateMachine _stateMachine)
    {

    }
}
