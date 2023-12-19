using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InvincibleState : IPlayerState
{
    private Movements movements;
    private CollectObjects collectObjects;
    private PushOtherPlayers pushOtherPlayers;
    private PlayerStateMachine stateMachine;
    private PlayerInvincibility playerInvincibility;

    public void OnEnter(PlayerStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
        _stateMachine.playerInput.onActionTriggered += this.OnAction;
        movements = _stateMachine.movements;
        collectObjects = _stateMachine.collectObjects;
        pushOtherPlayers = _stateMachine.pushOtherPlayers;

        //Launch invincibility
        playerInvincibility = _stateMachine.playerInvincibility;
        playerInvincibility.LaunchInvincibility();
    }

    public void OnExit(PlayerStateMachine _stateMachine)
    {
        playerInvincibility.StopInvincibility();
    }

    private void OnAction(InputAction.CallbackContext context)
    {
        if (this == stateMachine.currentState)
        {
            //In default state, player can move, collect an objet and push
            switch (context.action.name)
            {
                case "Movements":
                    movements.Move(context.action.ReadValue<Vector2>());
                    break;
                case "InteractWithObjects":
                    if (context.started)
                    {
                        collectObjects.TryToCollectObject();
                    }
                    break;
                case "PushOtherPlayers":
                    if (context.started)
                    {
                        pushOtherPlayers.TryToPush();
                        //If player in invincibility try to push other player, it stop invincibility
                        stateMachine.ChangeState(stateMachine.defaultState);
                    }
                    break;
            }
        }
    }
}
