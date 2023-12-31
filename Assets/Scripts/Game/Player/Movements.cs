using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movements : MonoBehaviour
{
    private Vector3 _lastOrientation;
    private Vector3 _actualOrientation;

    private Rigidbody _rb;
    public float defaultMoveSpeed;
    [HideInInspector]
    public float actualSpeed;

    [HideInInspector]
    public bool isInMovement;

    private Animator _animator;

    public bool isOnGrass;
    public bool isOnRock;
    public bool isOnWood;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<PlayerStateMachine>().playerAnimator;

        _lastOrientation = transform.forward;
        _actualOrientation = transform.forward;
    }

    public void Move(Vector2 value)
    {
        if (!GameManager.Instance.isGameOver && !GameManager.Instance.isPause)
        {
            _lastOrientation = _actualOrientation;

            //If joystick is not in neutral pos, actual orientation is the same as the joystick
            if (value != new Vector2(0, 0))
            {
                _actualOrientation = new Vector3(value.x, 0f, value.y);
                isInMovement = true;

                _animator.SetInteger("State", 1);
            }
            //Else keep the last orientation to don't go to the neutral pos
            else
            {
                _actualOrientation = _lastOrientation;
                isInMovement = false;
                _animator.SetInteger("State", 0);
            }

            //Player orientation is the same as the stick
            transform.forward = _actualOrientation;
        }
    }

    private void FixedUpdate()
    {
        UpdateGroundType();

        if (isInMovement && !GameManager.Instance.isGameOver && !GameManager.Instance.isPause)
        {
            //Player moves when joystick is held
            Vector3 velocity = _actualOrientation * actualSpeed * Time.deltaTime;
            _rb.velocity = velocity;
        }
    }

    private void UpdateGroundType()
    {
        //Check on what type of ground player walks
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, 2))
        {
            switch (hit.collider.gameObject.tag)
            {
                case "Grass":
                    isOnGrass = true;
                    isOnRock = false;
                    isOnWood = false;
                    break;
                case "Rock":
                    isOnGrass = false;
                    isOnRock = true;
                    isOnWood = false;
                    break;
                case "Wood":
                    isOnGrass = false;
                    isOnRock = false;
                    isOnWood = true;
                    break;
                default:
                    isOnGrass = false;
                    isOnRock = false;
                    isOnWood = false;
                    break;
            }
        }
        else
        {
            isOnGrass = false;
            isOnRock = false;
            isOnWood = false;
        }
    }
}
