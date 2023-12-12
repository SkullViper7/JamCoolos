using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NumberChoice : MonoBehaviour
{
    [SerializeField]
    private GameObject numberOfPlayersWindow;
    [SerializeField]
    private GameObject lobbyWindow;
    [SerializeField]
    private PlayerInputManager playerInputManager;

    public void Click(int _numberOfPlayer)
    {
        //Set the max number of player and show the lobby screen
        GameManager.Instance.maxPlayerCount = _numberOfPlayer;
        lobbyWindow.SetActive(true);
        playerInputManager.EnableJoining();
        numberOfPlayersWindow.SetActive(false);
    }
}