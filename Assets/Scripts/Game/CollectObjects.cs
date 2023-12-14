using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CollectObjects : MonoBehaviour
{
    private PlayerPerimeter playerPerimeter;

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

                if (Vector3.Distance(transform.position, currentObject.transform.position) <= 2)
                {
                    if (Vector2.Angle(new Vector2(transform.forward.x, transform.forward.z), new Vector2(currentObject.transform.position.x - transform.position.x, currentObject.transform.position.z - transform.position.z)) <= 35f)
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

            
        }

        
    }
}