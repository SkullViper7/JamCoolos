using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HoldingState : IPlayerState
{
    private Movements _movements;
    private CollectObjects _collectObjects;
    private PlayerStateMachine _stateMachine;

    public void OnEnter(PlayerStateMachine stateMachine)
    {
        this._stateMachine = stateMachine;
        stateMachine.playerInput.onActionTriggered += this.OnAction;
        _movements = stateMachine.movements;
        _collectObjects = stateMachine.collectObjects;

        //Set the actual move speed depending of the weight of the object held
        _movements.actualSpeed = _movements.defaultMoveSpeed * SpeedCoefficient();
    }

    public void OnExit(PlayerStateMachine stateMachine)
    {
        
    }

    private void OnAction(InputAction.CallbackContext context)
    {
        if (this == _stateMachine.currentState)
        {
            //Player who's holding an object can only move and drop it
            switch (context.action.name)
            {
                case "Movements":
                    _movements.Move(context.action.ReadValue<Vector2>());
                    break;
                case "InteractWithObjects":
                    if (context.canceled)
                    {
                        _collectObjects.DropObject();
                    }
                    break;
            }
        }
    }

    private float SpeedCoefficient()
    {
        //Return the multiplier to apply to the default speed to reduce speed
        return 1f - (_collectObjects.objectThatIsHeld.GetComponent<CollectableObject>().weight / 100f);
    }
}
