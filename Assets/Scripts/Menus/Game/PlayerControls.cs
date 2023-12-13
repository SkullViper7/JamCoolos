using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    private PlayerInput playerInput;

    private Vector3 lastOrientation;
    private Vector3 actualOrientation;

    public float moveSpeed;
    private bool isInMovement;

    private void Start()
    {
        LinkPlayerToDevice();
    }

    private void LinkPlayerToDevice()
    {
        //Determine which PlayerInputController to find depending of the name of the player
        switch (gameObject.name)
        {
            case "Player1":
                TryToFindController("PlayerInputController1");
                break;
            case "Player2":
                TryToFindController("PlayerInputController2");
                break;
            case "Player3":
                TryToFindController("PlayerInputController3");
                break;
            case "Player4":
                TryToFindController("PlayerInputController4");
                break;
            default:
                //For the tests player
                playerInput = GetComponent<PlayerInput>();
                playerInput.onActionTriggered += OnAction;
                break;
        }
    }

    private void TryToFindController(string _name)
    {
        //Try to find the PlayerInputController for the player given, if there is no PlayerInputController for it, desactive it
        if (GameObject.Find(_name) != null)
        {
            playerInput = GameObject.Find(_name).GetComponent<PlayerInput>();
            playerInput.onActionTriggered += OnAction;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void OnAction(InputAction.CallbackContext context)
    {
        //List of all inputs for the player
        switch (context.action.name)
        {
            case "Movements":
                Move(context.action.ReadValue<Vector2>());
                break;
            case "InteractWithObjects":
                Debug.Log("interact");
                break;
            case "PushOtherPlayers":
                Debug.Log("push");
                break;
        }
    }

    private void Move(Vector2 _value)
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

            transform.Translate(velocity, Space.World);
        }
    }
}