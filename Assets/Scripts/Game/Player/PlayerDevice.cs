using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDevice : MonoBehaviour
{
    [HideInInspector]public PlayerInput playerInput;
    private StateMachine stateMachine;

    private void Start()
    {
        //Assign device
        stateMachine = GetComponent<StateMachine>();
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
                stateMachine.playerInput = playerInput;
                stateMachine.ChangeState(stateMachine.defaultState);
                break;
        }
    }

    private void TryToFindController(string _name)
    {
        //Try to find the PlayerInputController for the player given, if there is no PlayerInputController for him, desactive him
        if (GameObject.Find(_name) != null)
        {
            playerInput = GameObject.Find(_name).GetComponent<PlayerInput>();
            stateMachine.playerInput = playerInput;
            stateMachine.ChangeState(stateMachine.defaultState);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}