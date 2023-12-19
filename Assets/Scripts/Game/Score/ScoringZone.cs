using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoringZone : MonoBehaviour
{
    public GameObject playerAssignToThisZone;

    private void OnTriggerEnter(Collider other)
    {
        //If there is an object in the zone
        if (other.CompareTag("CollectableObject"))
        {
            //If this object is dropped by the good player
            CollectableObject collectableObject = other.GetComponent<CollectableObject>();
            if (collectableObject.lastPlayerWhoHeldThisObject == playerAssignToThisZone)
            {
                //Add score to the player
                ScoreManager.Instance.AddScore(playerAssignToThisZone.name, collectableObject.score);

                //Remove object from all player perimeters
                RemoveObjectFromAllPerimeters(other.gameObject);

                //Release object
                collectableObject.poolWhereItCameFrom.Release(other.gameObject);
            }
        }
    }

    private void RemoveObjectFromAllPerimeters(GameObject _object)
    {
        //For each player in game
        GameManager gameManager = GameManager.Instance;
        for (int i = 0; i < gameManager.players.Count; i++)
        {
            //If player perimeter contains object given
            PlayerPerimeter playerPerimeter = gameManager.players[i].GetComponentInChildren<PlayerPerimeter>();
            if(playerPerimeter.collectableObjectsInPerimeter.Contains(_object))
            {
                //Remove object
                playerPerimeter.collectableObjectsInPerimeter.Remove(_object);
            }
        }
    }
}