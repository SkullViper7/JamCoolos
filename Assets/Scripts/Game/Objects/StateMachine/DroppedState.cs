using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedState : IObjectState
{
    private ObjectStateMachine _objectStateMachine;
    private ObjectFall _objectFall;

    public void OnEnter(ObjectStateMachine objectStateMachine)
    {
        this._objectStateMachine = objectStateMachine;

        //Listen if the object has hit the ground
        _objectFall = objectStateMachine.objectFall;
        _objectFall.enabled = true;
        _objectFall.ObjectHitsTheGround += ObjectHasHitTheGround;

        //Drop object on the ground
        objectStateMachine.transform.SetParent(null);

        Rigidbody rigidbody = objectStateMachine.GetComponent<Rigidbody>();

        rigidbody.isKinematic = false;
        rigidbody.AddForce(objectStateMachine.transform.up * objectStateMachine.dropUpForce);
        rigidbody.AddForce(objectStateMachine.transform.forward * objectStateMachine.dropForwardForce);
    }

    public void OnExit(ObjectStateMachine objectStateMachine)
    {
        _objectFall.enabled = false;
    }

    private void ObjectHasHitTheGround()
    {
        //Object is collectable again
        _objectStateMachine.ChangeState(_objectStateMachine.collectableState);
    }
}
