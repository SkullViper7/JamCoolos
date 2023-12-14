using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    public PlayerInput playerInput;
    public Movements movements;
    public CollectObjects collectObjects;

    private void Start()
    {
        LinkPlayerToDevice();
        movements = GetComponent<Movements>();
        collectObjects = GetComponent<CollectObjects>();
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
                movements.Move(context.action.ReadValue<Vector2>());
                break;
            case "InteractWithObjects":
                if (context.started)
                {
                    collectObjects.Interact();
                }
                break;
            case "PushOtherPlayers":
                Debug.Log("push");
                break;
        }
    }
}