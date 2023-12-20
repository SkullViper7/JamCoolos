using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InvincibleState : IPlayerState
{
    private Movements _movements;
    private CollectObjects _collectObjects;
    private PushOtherPlayers _pushOtherPlayers;
    private PlayerStateMachine _stateMachine;
    private PlayerInvincibility _playerInvincibility;

    public void OnEnter(PlayerStateMachine stateMachine)
    {
        this._stateMachine = stateMachine;
        stateMachine.playerInput.onActionTriggered += this.OnAction;
        _movements = stateMachine.movements;
        _collectObjects = stateMachine.collectObjects;
        _pushOtherPlayers = stateMachine.pushOtherPlayers;

        //Set the speed by delfault
        _movements.actualSpeed = _movements.defaultMoveSpeed;

        //Launch invincibility
        _playerInvincibility = stateMachine.playerInvincibility;
        _playerInvincibility.LaunchInvincibility();
    }

    public void OnExit(PlayerStateMachine stateMachine)
    {
        _playerInvincibility.StopInvincibility();
    }

    private void OnAction(InputAction.CallbackContext context)
    {
        if (this == _stateMachine.currentState)
        {
            //In default state, player can move, collect an objet and push
            switch (context.action.name)
            {
                case "Movements":
                    _movements.Move(context.action.ReadValue<Vector2>());
                    break;
                case "InteractWithObjects":
                    if (context.started)
                    {
                        _collectObjects.TryToCollectObject();
                    }
                    break;
                case "PushOtherPlayers":
                    if (context.started)
                    {
                        _pushOtherPlayers.TryToPush();
                        //If player in invincibility try to push other player, it stop invincibility
                        _stateMachine.ChangeState(_stateMachine.defaultState);
                    }
                    break;
            }
        }
    }
}
