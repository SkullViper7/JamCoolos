using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsHeldState : IObjectState
{
    private CollectableObject collectableObject;

    public void OnEnter(ObjectStateMachine _objectStateMachine)
    {
        collectableObject = _objectStateMachine.GetComponent<CollectableObject>();

        //object is collected
        _objectStateMachine.transform.SetParent(collectableObject.actualPlayerWhoHoldThisObject.transform);
        _objectStateMachine.GetComponent<Rigidbody>().isKinematic = true;

        //Set a new height depending of the size of the object
        float newHeight = collectableObject.actualPlayerWhoHoldThisObject.GetComponent<MeshRenderer>().bounds.size.y / 2f + _objectStateMachine.GetComponent<MeshRenderer>().bounds.size.y / 2;

        _objectStateMachine.transform.localPosition = new Vector3(0, newHeight, 0);
        _objectStateMachine.transform.localRotation = Quaternion.identity;
    }

    public void OnExit(ObjectStateMachine _objectStateMachine)
    {
        //Set the historic of the object
        collectableObject.lastPlayerWhoHeldThisObject = collectableObject.actualPlayerWhoHoldThisObject;
        collectableObject.actualPlayerWhoHoldThisObject = null;
    }
}