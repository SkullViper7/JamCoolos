using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusManager : MonoBehaviour
{
    int randomInt;

    private void OnTriggerEnter(Collider other)
    {
        randomInt = Random.Range(0, 2);

        if (other.gameObject.tag == "Player")
        {
            switch (randomInt)
            {
                case 0: StartCoroutine(MoveFaster(other.gameObject));
                    break;
                case 1: StartCoroutine(Invincible());
                    break;
                case 2: StartCoroutine(Force());
                    break;
            }
        }

        gameObject.GetComponent<MeshRenderer>().enabled = false;
    }

    IEnumerator MoveFaster(GameObject player)
    {
        player.GetComponent<PlayerControls>().movements.moveSpeed = 10;
        
        yield return new WaitForSeconds(3);

        player.GetComponent<PlayerControls>().movements.moveSpeed = 5;
        Destroy(gameObject);
    }

    IEnumerator Invincible()
    {
        Destroy(gameObject);

        yield return null;
    }

    IEnumerator Force()
    {
        Destroy(gameObject);

        yield return null;
    }
}