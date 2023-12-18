using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DefaultState : IPlayerState
{
    private Movements movements;
    private CollectObjects collectObjects;
    private PushOtherPlayers pushOtherPlayers;
    private PlayerStateMachine stateMachine;

    public void OnEnter(PlayerStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
        _stateMachine.playerInput.onActionTriggered += this.OnAction;
        movements = _stateMachine.movements;
        collectObjects = _stateMachine.collectObjects;
        pushOtherPlayers = _stateMachine.pushOtherPlayers;

        //Set the speed by delfault
        movements.actualSpeed = movements.defaultMoveSpeed;
    }

    public void OnExit(PlayerStateMachine _stateMachine)
    {

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
                    }
                    break;
            }
        }
    }
}
