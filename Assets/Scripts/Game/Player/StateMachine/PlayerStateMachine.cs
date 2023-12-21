using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateMachine : MonoBehaviour
{
    public IPlayerState currentState;

    public DefaultState defaultState = new();
    public RecoveryState recoveryState = new();
    public HoldingState holdingState = new();
    public FallingState fallingState = new();
    public InvincibleState invincibleState = new();

    [HideInInspector] public PlayerInput playerInput;
    [HideInInspector] public Movements movements;
    [HideInInspector] public CollectObjects collectObjects;
    [HideInInspector] public PushOtherPlayers pushOtherPlayers;
    [HideInInspector] public PlayerFall playerFall;
    [HideInInspector] public PlayerRecovery playerRecovery;
    [HideInInspector] public PlayerInvincibility playerInvincibility;

    public Animator playerAnimator;

    private void Awake()
    {
        movements = GetComponent<Movements>();
        collectObjects = GetComponent<CollectObjects>();
        pushOtherPlayers = GetComponent<PushOtherPlayers>();
        playerFall = GetComponent<PlayerFall>();
        playerRecovery = GetComponent<PlayerRecovery>();
        playerInvincibility = GetComponent<PlayerInvincibility>();
    }

    public void ChangeState(IPlayerState newState)
    {
        //Switch to a new state
        if (currentState != null)
        {
            currentState.OnExit(this);
        }

        currentState = newState;
        currentState.OnEnter(this);
    }
}

//Interface for each state
public interface IPlayerState
{
    public void OnEnter(PlayerStateMachine playerStateMachine);
    public void OnExit(PlayerStateMachine playerStateMachine);
}
