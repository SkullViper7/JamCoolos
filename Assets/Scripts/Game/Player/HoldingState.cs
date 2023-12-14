using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HoldingState : IState
{
    private Movements movements;
    public CollectObjects collectObjects;
    private StateMachine stateMachine;

    public void OnEnter(StateMachine _stateMachine)
    {
        Debug.Log("HoldingState");

        stateMachine = _stateMachine;
        _stateMachine.playerInput.onActionTriggered += this.OnAction;
        movements = _stateMachine.movements;
        collectObjects = _stateMachine.collectObjects;
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
            //Player who's holding an object can only move
            switch (context.action.name)
            {
                case "Movements":
                    movements.Move(context.action.ReadValue<Vector2>());
                    break;
                case "InteractWithObjects":
                    if (context.canceled)
                    {
                        collectObjects.DropObject();
                    }
                    break;
            }
        }
    }
}
