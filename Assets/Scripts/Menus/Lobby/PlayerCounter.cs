using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCounter : MonoBehaviour
{
    private PlayerInputManager playerInputManager;
    [SerializeField]
    private GameObject launchGameButton;
    [SerializeField]
    private GameObject instructions;
    [SerializeField]
    private List<GameObject> playerHolders = new();

    private void Start()
    {
        playerInputManager = GetComponent<PlayerInputManager>();
    }

    public void OnPlayerConneted()
    {
        //Add a player in the count if a player joines and disable joining if the maximum of players is reached
        if (GameManager.Instance.playerCount + 1 >= GameManager.Instance.maxPlayerCount)
        {
            playerInputManager.DisableJoining();
            GameManager.Instance.playerCount++;
        }
        else
        {
            GameManager.Instance.playerCount++;
        }

        //Active the player holder associated to the player who has joined
        playerHolders[GameManager.Instance.playerCount - 1].SetActive(true);

        if (GameManager.Instance.playerCount == GameManager.Instance.maxPlayerCount)
        {
            //Active the button to launch the game when everyone is connected
            instructions.SetActive(false);
            launchGameButton.SetActive(true);
        }
    }
}
