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

    public PlayerInput playerInput;
    public Movements movements;
    public CollectObjects collectObjects;
    public PushOtherPlayers pushOtherPlayers;
    public PlayerFall playerFall;

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
