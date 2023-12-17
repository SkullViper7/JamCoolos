using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;

public class CollectObjects : MonoBehaviour
{
    private PlayerStateMachine playerStateMachine;
    private PlayerPerimeter playerPerimeter;
    /*[HideInInspector]*/public GameObject objectThatIsHeld;

    public float dropUpForce;
    public float dropForwardForce;

    void Start()
    {
        playerStateMachine = GetComponent<PlayerStateMachine>();
        playerPerimeter = GetComponentInChildren<PlayerPerimeter>();
    }

    public void TryToCollectObject()
    {
        //If there is objects in player perimeter
        if (playerPerimeter.collectableObjectsInPerimeter != null && playerPerimeter.collectableObjectsInPerimeter.Count != 0)
        {
            //For each object in player perimeter
            for (int i = 0; i < playerPerimeter.collectableObjectsInPerimeter.Count; i++)
            {
                //If there is no object already held
                if (objectThatIsHeld == null)
                {
                    GameObject currentObject = playerPerimeter.collectableObjectsInPerimeter[i];

                    //If object is enough close
                    if (Vector3.Distance(transform.position, currentObject.transform.position) <= 2)
                    {
                        //If object is in front of the player
                        if (Vector2.Angle(new Vector2(transform.forward.x, transform.forward.z), new Vector2(currentObject.transform.position.x - transform.position.x, currentObject.transform.position.z - transform.position.z)) <= 35f)
                        {
                            ObjectStateMachine objectStateMachine = currentObject.GetComponent<ObjectStateMachine>();

                            //If object is collectable
                            if (objectStateMachine.currentState == objectStateMachine.collectableState)
                            {
                                //Collect object and switch to holding state
                                CollectObject(currentObject);
                            }
                        }
                    }
                }
            }
        }
    }

    private void CollectObject(GameObject _object)
    {
        //Set the actual player who hold the object and the object that is held
        _object.GetComponent<CollectableObject>().actualPlayerWhoHoldThisObject = this.gameObject;
        objectThatIsHeld = _object;

        //Set the different state machines
        playerStateMachine.ChangeState(playerStateMachine.holdingState);
        ObjectStateMachine objectStateMachine = _object.GetComponent<ObjectStateMachine>();
        objectStateMachine.ChangeState(objectStateMachine.isHeldState);
    }

    public void DropObject()
    {
        //Set the different state machines
        playerStateMachine.ChangeState(playerStateMachine.defaultState);
        ObjectStateMachine objectStateMachine = objectThatIsHeld.GetComponent<ObjectStateMachine>();
        objectStateMachine.dropUpForce = this.dropUpForce;
        objectStateMachine.dropForwardForce = this.dropForwardForce;
        objectStateMachine.ChangeState(objectStateMachine.droppedState);

        objectThatIsHeld = null;
    }
}