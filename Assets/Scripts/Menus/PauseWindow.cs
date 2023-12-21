using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Linq;

public class PauseWindow : MonoBehaviour
{
    private Dictionary<PlayerInput, bool> _playerInputs = new();

    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = GameManager.Instance;

        for (int i = 0; i < _gameManager.players.Count; i++)
        {
            //Get all playerInputs 
            PlayerStateMachine _playerStateMachine = _gameManager.players[i].GetComponent<PlayerStateMachine>();
            _playerInputs.Add(_gameManager.players[i].GetComponent<PlayerStateMachine>().playerInput, false);
        }

        for (int i = 0; i < _playerInputs.Count; i++)
        {
            //Listen all players
            var kvp = _playerInputs.ElementAt(i);

            kvp.Key.onActionTriggered += this.OnAction;
        }
    }

    private void OnAction(InputAction.CallbackContext context)
    {
        //Pause game
        if (!GameManager.Instance.isGameOver)
        {
            switch (context.action.name)
            {
                case "ReadyToPlay":
                    if (context.started)
                    {
                        OnPlayerPress(context);
                    }
                    else if (context.canceled)
                    {
                        OnLeftPress(context);
                    }
                    break;
            }
        }
    }

    private void OnPlayerPress(InputAction.CallbackContext context)
    {
        Debug.Log(context.control.device.deviceId);
        //switch (context.control.device.displayName)
    }

    private void OnLeftPress(InputAction.CallbackContext context)
    {
        
    }

    private void PlayGame()
    {
        //Play the game
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }
}
