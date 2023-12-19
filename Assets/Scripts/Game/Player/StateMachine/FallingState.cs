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

        if (playerFall.objectThatPushedMe.CompareTag("Player"))
        {
            //Player falls
            playerFall.Fall(playerFall.objectThatPushedMe.transform.forward);
        }
        else
        {
            int randomAngle = Random.Range(0, 2);

            if (randomAngle == 0)
            {
                playerFall.Fall(playerFall.objectThatPushedMe.transform.forward);
            }
            if (randomAngle == 1)
            {
                playerFall.Fall(-playerFall.objectThatPushedMe.transform.forward);
            }
        }

        //If player held an object, he drops it
        if (collectObjects.objectThatIsHeld != null)
        {
            playerFall.DropObjectWhenPlayerFalls(collectObjects.objectThatIsHeld);
        }
    }

    public void OnExit(PlayerStateMachine _stateMachine)
    {

    }
}
