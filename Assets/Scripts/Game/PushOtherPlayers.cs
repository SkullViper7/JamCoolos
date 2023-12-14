using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushOtherPlayers : MonoBehaviour
{
    public void Push()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, 2f))
        {
            if (hit.collider.CompareTag("Player"))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                Debug.Log("Did Hit");
            }
        }
    }
}
