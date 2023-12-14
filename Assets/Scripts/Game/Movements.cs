using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movements : MonoBehaviour
{
    private Vector3 lastOrientation;
    private Vector3 actualOrientation;

    Rigidbody rb;
    public float moveSpeed;
    public bool isInMovement;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Move(Vector2 _value)
    {
        lastOrientation = actualOrientation;

        //If joystick is not in neutral pos, actual orientation is the same as the joystick
        if (_value != new Vector2(0, 0))
        {
            actualOrientation = new Vector3(_value.x, 0f, _value.y);
            isInMovement = true;
        }
        //Else keep the last orientation to not go to the neutral pos
        else
        {
            actualOrientation = lastOrientation;
            isInMovement = false;
        }

        //Player orientation is the same as the stick
        transform.forward = actualOrientation;
    }

    private void FixedUpdate()
    {
        if (isInMovement)
        {
            //Player moves when joystick is held
            Vector3 velocity = actualOrientation * moveSpeed * Time.deltaTime;
            rb.velocity = velocity;
        }
    }
}
