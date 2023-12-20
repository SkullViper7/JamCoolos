using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsHeldState : IObjectState
{
    private CollectableObject _collectableObject;

    public void OnEnter(ObjectStateMachine objectStateMachine)
    {
        _collectableObject = objectStateMachine.collectableObject;

        //object is collected
        objectStateMachine.transform.SetParent(_collectableObject.actualPlayerWhoHoldThisObject.transform);
        objectStateMachine.GetComponent<Rigidbody>().isKinematic = true;

        //Set a new height depending of the size of the object
        float newHeight = _collectableObject.actualPlayerWhoHoldThisObject.GetComponent<MeshRenderer>().bounds.size.y / 2f + objectStateMachine.GetComponent<MeshRenderer>().bounds.size.y / 2;

        objectStateMachine.transform.localPosition = new Vector3(0, newHeight, 0);
        objectStateMachine.transform.localRotation = Quaternion.identity;
    }

    public void OnExit(ObjectStateMachine objectStateMachine)
    {
        //Set the historic of the object
        _collectableObject.lastPlayerWhoHeldThisObject = _collectableObject.actualPlayerWhoHoldThisObject;
        _collectableObject.actualPlayerWhoHoldThisObject = null;
    }
}