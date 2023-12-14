using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushOtherPlayers : MonoBehaviour
{
    public float punshForce;

    public void Push()
    {
        //Detecte if a player is in front of this player and close to push it
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 1f))
        {
            if (hit.collider.CompareTag("Player"))
            {
                //Other player falls
                hit.collider.GetComponent<PlayerFall>().Fall(transform.forward, punshForce);
            }
        }
    }
}
