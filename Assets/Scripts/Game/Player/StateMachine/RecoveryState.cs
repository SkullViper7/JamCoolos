using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RecoveryState : IPlayerState
{
    private Movements movements;
    private CollectObjects collectObjects;
    private PushOtherPlayers pushOtherPlayers;
    private PlayerStateMachine stateMachine;
    private PlayerRecovery playerRecovery;

    public void OnEnter(PlayerStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
        _stateMachine.playerInput.onActionTriggered += this.OnAction;
        movements = _stateMachine.movements;
        collectObjects = _stateMachine.collectObjects;
        pushOtherPlayers = _stateMachine.pushOtherPlayers;

        //Launch recovery
        playerRecovery = _stateMachine.playerRecovery;
        playerRecovery.LaunchRecovery(playerRecovery.defaultRecoveryTime * TimeCoefficient());

        collectObjects.objectThatIsHeld = null;
    }

    public void OnExit(PlayerStateMachine _stateMachine)
    {
        playerRecovery.StopRecovery();
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

    private float TimeCoefficient()
    {
        //Return the multiplier to apply to the default recovery time to reduce it
        return collectObjects.objectThatIsHeld.GetComponent<CollectableObject>().weight / 100f;
    }
}