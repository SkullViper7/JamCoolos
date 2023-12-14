using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFall : MonoBehaviour
{
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Fall(Vector3 _direction, float _punshForce)
    {
        //Stop any movement
        PlayerControls player = GetComponent<PlayerControls>();
        player.isStunned = true;
        player.movements.isInMovement = false;

        //Player falls
        Vector3 force = _direction * _punshForce;
        rb.AddForce(force);
        StartCoroutine(WaitUntilRaise());
    }

    private IEnumerator WaitUntilRaise()
    {
        //Wait during the fall
        yield return new WaitForSeconds(2f);

        //Player can move again
        PlayerControls player = GetComponent<PlayerControls>();
        player.isStunned = false;
    }
}
