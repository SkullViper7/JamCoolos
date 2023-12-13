using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPerimeter : MonoBehaviour
{
    public List<GameObject> collectableObjectsInPerimeter = new();

    private void OnTriggerEnter(Collider other)
    {
        //Add collectable object when it enter the player perimeter
        collectableObjectsInPerimeter.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        //Remove collectable object when it exit the player perimeter
        collectableObjectsInPerimeter.Remove(other.gameObject);
    }
}
