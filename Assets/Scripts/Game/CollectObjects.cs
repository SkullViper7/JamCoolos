using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CollectObjects : MonoBehaviour
{
    private PlayerPerimeter playerPerimeter;
    private GameObject myObject;

    void Start()
    {
        playerPerimeter = GetComponentInChildren<PlayerPerimeter>();
        myObject = playerPerimeter.collectableObjectsInPerimeter[0];

    }

    public void Interact()
    {
        if (Vector3.Distance(transform.position, myObject.transform.position) <= 2)
        {
            if (Vector2.Angle(new Vector2(transform.forward.x, transform.forward.z), new Vector2(myObject.transform.position.x - transform.position.x, myObject.transform.position.z - transform.position.z)) <= 35f)
            {
                Debug.Log("CanCollect");
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

    private void FixedUpdate()
    {
        if (myObject.transform.position != null)
        {
            Debug.DrawLine(transform.position, myObject.transform.position, Color.blue, 0.01f);
        }

        Debug.DrawRay(transform.position, transform.forward, Color.green, 0.01f);
    }
}
