using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HoldingState : IPlayerState
{
    private Movements movements;
    private CollectObjects collectObjects;
    private PlayerStateMachine stateMachine;

    public void OnEnter(PlayerStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
        _stateMachine.playerInput.onActionTriggered += this.OnAction;
        movements = _stateMachine.movements;
        collectObjects = _stateMachine.collectObjects;

        //Set the actual move speed depending of the weight of the object held
        movements.actualSpeed = movements.defaultMoveSpeed * SpeedCoefficient();
    }

    public void OnExit(PlayerStateMachine _stateMachine)
    {

    }

    private void OnAction(InputAction.CallbackContext context)
    {
        if (this == stateMachine.currentState)
        {
            //Player who's holding an object can only move and drop it
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

    private float SpeedCoefficient()
    {
        //Return the multiplier to apply to the default speed to reduce speed
        return 1f - (collectObjects.objectThatIsHeld.GetComponent<CollectableObject>().weight / 100f);
    }
}
