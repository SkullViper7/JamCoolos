using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPerimeter : MonoBehaviour
{
    public List<GameObject> collectableObjectsInPerimeter = new();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CollectableObject"))
        {
            //Add collectable object when it enter the player perimeter
            collectableObjectsInPerimeter.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CollectableObject"))
        {
            //Remove collectable object when it exit the player perimeter
            collectableObjectsInPerimeter.Remove(other.gameObject);
        }
    }
}
