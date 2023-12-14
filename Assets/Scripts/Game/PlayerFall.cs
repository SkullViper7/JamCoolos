using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFall : MonoBehaviour
{
    private Rigidbody rb;
    private Movements movements;
    private StateMachine stateMachine;
    private CollectObjects collectObjects;

    public GameObject playerThatHavePushingMe;
    public float punshForce;
    public float dropUpForce;
    public float dropForwardForce;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        movements = GetComponent<Movements>();
        stateMachine = GetComponent<StateMachine>();
        collectObjects = GetComponent<CollectObjects>();
    }

    public void Fall(Vector3 _direction)
    {
        //Stop any movement
        movements.isInMovement = false;

        //Player falls
        transform.forward = _direction;
        Vector3 force = transform.forward * punshForce;
        rb.AddForce(force);
        StartCoroutine(WaitUntilRaise());
    }

    public void DropObjectWhenPlayerFalls(GameObject _objectThatIsHeld)
    {
        //player drop the object when it is falls
        _objectThatIsHeld.transform.SetParent(null);

        Rigidbody objectThatIsHeldRigidbody = _objectThatIsHeld.GetComponent<Rigidbody>();

        objectThatIsHeldRigidbody.isKinematic = false;
        objectThatIsHeldRigidbody.AddForce(_objectThatIsHeld.transform.up * dropUpForce);
        objectThatIsHeldRigidbody.AddForce(_objectThatIsHeld.transform.forward * dropForwardForce);

        collectObjects.objectThatIsHeld = null;
    }

    private IEnumerator WaitUntilRaise()
    {
        //Wait during the fall
        yield return new WaitForSeconds(2f);

        //Player can move again
        stateMachine.ChangeState(stateMachine.defaultState);
    }
}
