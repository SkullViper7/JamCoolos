using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;

public class CollectObjects : MonoBehaviour
{
    private StateMachine stateMachine;
    private PlayerPerimeter playerPerimeter;
    public GameObject objectThatIsHeld;

    public float dropUpForce;
    public float dropForwardForce;

    void Start()
    {
        stateMachine = GetComponent<StateMachine>();
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
                GameObject currentObject = playerPerimeter.collectableObjectsInPerimeter[i];

                //If object is enough close
                if (Vector3.Distance(transform.position, currentObject.transform.position) <= 2)
                {
                    //If object is in front of the player
                    if (Vector2.Angle(new Vector2(transform.forward.x, transform.forward.z), new Vector2(currentObject.transform.position.x - transform.position.x, currentObject.transform.position.z - transform.position.z)) <= 35f)
                    {
                        //If there is no object already held
                        if (objectThatIsHeld == null)
                        {
                            //Collect object and switch to holding state
                            CollectObject(currentObject);
                        }
                    }
                    else
                    {
                        Debug.Log("Can not collect");
                    }
                }
                else
                {
                    Debug.Log("Can not collect");
                }
            }
        }
        else
        {
            Debug.Log("No object");
        }
    }

    private void CollectObject(GameObject _object)
    {
        stateMachine.ChangeState(stateMachine.holdingState);

        //Collect the object
        _object.transform.SetParent(transform);
        _object.GetComponent<Rigidbody>().isKinematic = true;
        _object.transform.localPosition = new Vector3(0, 2, 0);
        _object.transform.localRotation = Quaternion.identity;
        objectThatIsHeld = _object;
    }

    public void DropObject()
    {
        stateMachine.ChangeState(stateMachine.defaultState);

        //Drop object on the ground
        objectThatIsHeld.transform.SetParent(null);

        Rigidbody objectThatIsHeldRigidbody = objectThatIsHeld.GetComponent<Rigidbody>();

        objectThatIsHeldRigidbody.isKinematic = false;
        objectThatIsHeldRigidbody.AddForce(objectThatIsHeld.transform.up * dropUpForce);
        objectThatIsHeldRigidbody.AddForce(objectThatIsHeld.transform.forward * dropForwardForce);

        objectThatIsHeld = null;
    }
}