using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movements : MonoBehaviour
{
    private Vector3 lastOrientation;
    private Vector3 actualOrientation;

    private Rigidbody rb;
    public float defaultMoveSpeed;
    [HideInInspector]
    public float actualSpeed;

    [HideInInspector]
    public bool isInMovement;

    Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }

    public void Move(Vector2 _value)
    {
        lastOrientation = actualOrientation;

        //If joystick is not in neutral pos, actual orientation is the same as the joystick
        if (_value != new Vector2(0, 0))
        {
            actualOrientation = new Vector3(_value.x, 0f, _value.y);
            isInMovement = true;
            animator.SetInteger("State", 1);
        }
        //Else keep the last orientation to don't go to the neutral pos
        else
        {
            actualOrientation = lastOrientation;
            isInMovement = false;
            animator.SetInteger("State", 0);
        }

        //Player orientation is the same as the stick
        transform.forward = actualOrientation;
    }

    private void FixedUpdate()
    {
        if (isInMovement)
        {
            //Player moves when joystick is held
            Vector3 velocity = actualOrientation * actualSpeed * Time.deltaTime;
            rb.velocity = velocity;
        }
    }
}
