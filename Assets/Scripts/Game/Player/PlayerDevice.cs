using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDevice : MonoBehaviour
{
    [HideInInspector]public PlayerInput playerInput;
    private PlayerStateMachine playerStateMachine;

    private void Start()
    {
        //Assign device
        playerStateMachine = GetComponent<PlayerStateMachine>();
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
                playerStateMachine.playerInput = playerInput;
                playerStateMachine.ChangeState(playerStateMachine.defaultState);
                break;
        }
    }

    private void TryToFindController(string _name)
    {
        //Try to find the PlayerInputController for the player given, if there is no PlayerInputController for him, desactive him
        if (GameObject.Find(_name) != null)
        {
            playerInput = GameObject.Find(_name).GetComponent<PlayerInput>();
            playerStateMachine.playerInput = playerInput;
            playerStateMachine.ChangeState(playerStateMachine.defaultState);
            ScoreManager.Instance.playerScores.Add(this.gameObject.name, 0);
            GameManager.Instance.players.Add(this.gameObject);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}