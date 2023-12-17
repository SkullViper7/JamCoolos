using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFall : MonoBehaviour
{
    private Rigidbody rb;
    private Movements movements;
    private PlayerStateMachine stateMachine;
    private CollectObjects collectObjects;

    [HideInInspector]public GameObject playerThatPushedMe;
    public float pushForce;
    public float dropUpForce;
    public float dropForwardForce;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        movements = GetComponent<Movements>();
        stateMachine = GetComponent<PlayerStateMachine>();
        collectObjects = GetComponent<CollectObjects>();
    }

    public void Fall(Vector3 _direction)
    {
        //Stop any movement
        movements.isInMovement = false;

        //Player falls
        rb.drag = 2.5f;

        transform.forward = _direction;
        Vector3 force = transform.forward * pushForce;
        rb.AddForce(force);
        StartCoroutine(WaitUntilRaise());
    }

    public void DropObjectWhenPlayerFalls(GameObject _objectThatIsHeld)
    {
        //Set object state machine
        ObjectStateMachine objectStateMachine = _objectThatIsHeld.GetComponent<ObjectStateMachine>();
        objectStateMachine.dropUpForce = this.dropUpForce;
        objectStateMachine.dropForwardForce = this.dropForwardForce;
        objectStateMachine.ChangeState(objectStateMachine.droppedState);

        collectObjects.objectThatIsHeld = null;
    }

    private IEnumerator WaitUntilRaise()
    {
        //Wait during the fall
        yield return new WaitForSeconds(2f);

        rb.drag = 5f;
        playerThatPushedMe = null;

        //Player can move again
        stateMachine.ChangeState(stateMachine.defaultState);
    }
}
