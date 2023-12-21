using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPause : MonoBehaviour
{
    private PlayerStateMachine _stateMachine;
    [SerializeField]
    private GameObject _pauseWindow;

    public void InitializePause()
    {
        this._stateMachine = GetComponent<PlayerStateMachine>();
        _stateMachine.playerInput.onActionTriggered += this.OnAction;
    }

    private void OnAction(InputAction.CallbackContext context)
    {
        //Pause game
        if (!GameManager.Instance.isGameOver && !GameManager.Instance.isPause)
        {
            switch (context.action.name)
            {
                case "Pause":
                    PauseGame();
                    break;
            }
        }
    }

    private void PauseGame()
    {
        //Pause the game
        Time.timeScale = 0f;
        _pauseWindow.SetActive(true);
    }
}
