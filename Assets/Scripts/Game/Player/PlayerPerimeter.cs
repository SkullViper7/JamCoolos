using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPerimeter : MonoBehaviour
{
    [HideInInspector] public List<GameObject> collectableObjectsInPerimeter = new();
    [HideInInspector] public List<GameObject> playersInPerimeter = new();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CollectableObject"))
        {
            //Add collectable object when it enters the player perimeter
            collectableObjectsInPerimeter.Add(other.gameObject);
        }
        else if (other.CompareTag("Player"))
        {
            //Add player when he enters the player perimeter
            playersInPerimeter.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CollectableObject"))
        {
            //Remove collectable object when it exits the player perimeter
            collectableObjectsInPerimeter.Remove(other.gameObject);
        }
        else if (other.CompareTag("Player"))
        {
            //Remove player when he exits the player perimeter
            playersInPerimeter.Remove(other.gameObject);
        }
    }
}
