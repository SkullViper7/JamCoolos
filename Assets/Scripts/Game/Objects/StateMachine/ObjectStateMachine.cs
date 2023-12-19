using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectStateMachine : MonoBehaviour
{
    public IObjectState currentState;

    public CollectableState collectableState = new();
    public IsHeldState isHeldState = new();
    public DroppedState droppedState = new();

    [HideInInspector]
    public CollectableObject collectableObject;
    [HideInInspector]
    public ObjectFall objectFall;

    [HideInInspector]
    public float dropUpForce;
    [HideInInspector]
    public float dropForwardForce;

    private void Awake()
    {
        collectableObject = GetComponent<CollectableObject>();
        objectFall = GetComponent<ObjectFall>();

        ChangeState(collectableState);
    }

    public void ChangeState(IObjectState newState)
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
public interface IObjectState
{
    public void OnEnter(ObjectStateMachine _objectStateMachine);
    public void OnExit(ObjectStateMachine _objectStateMachine);
}