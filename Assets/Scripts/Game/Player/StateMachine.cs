using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class StateMachine : MonoBehaviour
{
    public IState currentState;

    public DefaultState defaultState = new();
    public HoldingState holdingState = new();
    public FallingState fallingState = new();

    [HideInInspector]public PlayerInput playerInput;
    [HideInInspector] public Movements movements;
    [HideInInspector] public CollectObjects collectObjects;
    [HideInInspector] public PushOtherPlayers pushOtherPlayers;
    [HideInInspector] public PlayerFall playerFall;

    private void Start()
    {
        movements = GetComponent<Movements>();
        collectObjects = GetComponent<CollectObjects>();
        pushOtherPlayers = GetComponent<PushOtherPlayers>();
        playerFall = GetComponent<PlayerFall>();
    }

    private void FixedUpdate()
    {
        //Execute the actual state's comportement
        if (currentState != null)
        {
            currentState.UpdateState(this);
        }
    }

    public void ChangeState(IState newState)
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
public interface IState
{
    public void OnEnter(StateMachine stateManager);
    public void UpdateState(StateMachine stateManager);
    public void OnExit(StateMachine stateManager);
}
