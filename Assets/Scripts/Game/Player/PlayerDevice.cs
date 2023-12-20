using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDevice : MonoBehaviour
{
    [HideInInspector]public PlayerInput playerInput;
    private PlayerStateMachine _playerStateMachine;

    private void Start()
    {
        //Assign device
        _playerStateMachine = GetComponent<PlayerStateMachine>();
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
                _playerStateMachine.playerInput = playerInput;
                _playerStateMachine.ChangeState(_playerStateMachine.defaultState);
                break;
        }
    }

    private void TryToFindController(string name)
    {
        //Try to find the PlayerInputController for the player given, if there is no PlayerInputController for him, desactive him
        if (GameObject.Find(name) != null)
        {
            playerInput = GameObject.Find(name).GetComponent<PlayerInput>();
            _playerStateMachine.playerInput = playerInput;
            _playerStateMachine.ChangeState(_playerStateMachine.defaultState);
            ScoreManager.Instance.playerScores.Add(this.gameObject.name, 0);
            GameManager.Instance.players.Add(this.gameObject);
            GameManager.Instance.gamepads.Add((Gamepad)playerInput.user.pairedDevices[0]);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}