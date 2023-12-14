using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;

public class CollectObjects : MonoBehaviour
{
    private PlayerPerimeter playerPerimeter;
    public int objectGrab = 0;
    public GameObject objectThatIsGrab;

    void Start()
    {
        playerPerimeter = GetComponentInChildren<PlayerPerimeter>();

    }

    public void Interact()
    {

        if (playerPerimeter.collectableObjectsInPerimeter != null && playerPerimeter.collectableObjectsInPerimeter.Count != 0)
        {

            for (int i = 0; i < playerPerimeter.collectableObjectsInPerimeter.Count; i++)
            {
                GameObject currentObject = playerPerimeter.collectableObjectsInPerimeter[i];
                Rigidbody rigidKinematic = currentObject.GetComponent<Rigidbody>();
                
                Transform parentTransform = playerPerimeter.transform;
                Transform childTransform = currentObject.transform;

                if (objectGrab != 0)
                {
                    Debug.Log("object Already grab");
                }
                else if (objectGrab == 0)
                {
                    if (Vector3.Distance(transform.position, currentObject.transform.position) <= 2)
                    {
                        if (Vector2.Angle(new Vector2(transform.forward.x, transform.forward.z), new Vector2(currentObject.transform.position.x - transform.position.x, currentObject.transform.position.z - transform.position.z)) <= 35f)
                        {
                            childTransform.SetParent(parentTransform);
                            
                            rigidKinematic.isKinematic = true;
                            currentObject.transform.localPosition = new Vector3(0,2,0);
                            currentObject.transform.localRotation = Quaternion.identity;
                            objectGrab++;
                            objectThatIsGrab = currentObject;
                            Debug.Log(objectGrab);
                        }
                        else
                        {
                            Debug.Log("CanNotCollect");
                        }
                    }
                    else
                    {
                        Debug.Log("CanNotCollect");
                    }
                }
            }   
        }
    }

    public void dropItem()
    {
        Rigidbody rigidKinematic = objectThatIsGrab.GetComponent<Rigidbody>();

        if (objectGrab == 1)
        {
            objectThatIsGrab.transform.SetParent(null);
            rigidKinematic.isKinematic = false;
            objectGrab = 0;
        }
    }
}