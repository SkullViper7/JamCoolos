using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    //Singleton
    private static GameManager _instance = null;
    public static GameManager Instance => _instance;
    //

    //[HideInInspector]
    public int maxPlayerCount;
    //[HideInInspector]
    public int playerCount;

    public bool isGameOver = true;
    public bool isPause = false;

    [HideInInspector]
    public List<GameObject> playerInputControls = new();

    [HideInInspector]
    public List<GameObject> players = new();
    public List<Gamepad> gamepads = new();

    private void Awake()
    {
        //Singleton
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            _instance = this;
        }
        //

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        playerCount = 0;
    }

    public void ResetManager()
    {
        players.Clear();
        gamepads.Clear();
        isGameOver = true;
    }

    public void ClearManager()
    {
        //Delete all playerInputControls
        foreach (GameObject playerInputControl in playerInputControls)
        {
            Destroy(playerInputControl);
        }
        playerInputControls.Clear();

        //Reset all values
        playerCount = 0;
        maxPlayerCount = 0;
        ResetManager();
    }
}