using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DefaultState : IState
{
    private Movements movements;
    private CollectObjects collectObjects;
    private PushOtherPlayers pushOtherPlayers;
    private StateMachine stateMachine;

    public void OnEnter(StateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
        _stateMachine.playerInput.onActionTriggered += this.OnAction;
        movements = _stateMachine.movements;
        collectObjects = _stateMachine.collectObjects;
        pushOtherPlayers = _stateMachine.pushOtherPlayers;
    }

    public void UpdateState(StateMachine _stateMachine)
    {

    }

    public void OnExit(StateMachine _stateMachine)
    {

    }

    public void OnAction(InputAction.CallbackContext context)
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
