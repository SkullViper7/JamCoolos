using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NumberChoice : MonoBehaviour
{
    [SerializeField]
    private PlayerInputManager playerInputManager;

    public void Click(int _numberOfPlayer)
    {
        //Set the max number of player and launch the lobby screen
        GameManager.Instance.maxPlayerCount = _numberOfPlayer;
        playerInputManager.EnableJoining();
    }
}