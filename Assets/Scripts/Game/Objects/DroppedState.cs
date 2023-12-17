using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedState : IObjectState
{
    ObjectStateMachine objectStateMachine;
    private ObjectFall objectFall;

    public void OnEnter(ObjectStateMachine _objectStateMachine)
    {
        objectStateMachine = _objectStateMachine;

        //Listen if the object has hit the ground
        objectFall = _objectStateMachine.GetComponent<ObjectFall>();
        objectFall.enabled = true;
        objectFall.ObjectHitsTheGround += ObjectHasHitTheGround;

        //Drop object on the ground
        _objectStateMachine.transform.SetParent(null);

        Rigidbody rigidbody = _objectStateMachine.GetComponent<Rigidbody>();

        rigidbody.isKinematic = false;
        rigidbody.AddForce(_objectStateMachine.transform.up * _objectStateMachine.dropUpForce);
        rigidbody.AddForce(_objectStateMachine.transform.forward * _objectStateMachine.dropForwardForce);
    }

    public void OnExit(ObjectStateMachine _objectStateMachine)
    {
        objectFall.enabled = false;
    }

    private void ObjectHasHitTheGround()
    {
        //Object is collectable again
        objectStateMachine.ChangeState(objectStateMachine.collectableState);
    }
}
