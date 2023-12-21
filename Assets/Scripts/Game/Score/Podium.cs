using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class Podium : MonoBehaviour
{
    public GameObject blocks;
    public Transform podium;
    public Transform first;
    public Transform second;
    public Transform third;
    public Transform fourth;

    [Header("Players")]
    public List<Transform> players;

    public void MoveToPodium()
    {
        ScoreManager scoreManager = ScoreManager.Instance;
        scoreManager.playerScores = scoreManager.playerScores.OrderByDescending(_player => _player.Value).ToDictionary(_player => _player.Key, _player => _player.Value);

        blocks.SetActive(true);

        int playerIndex = 0;
        foreach (var playerScore in scoreManager.playerScores)
        {
            Transform currentPlayer = players[playerIndex];
            currentPlayer.SetParent(podium);
            currentPlayer.position = GetPositionForPlayer(playerIndex); // Set the position based on score order
            playerIndex++;
        }
    }

    private Vector3 GetPositionForPlayer(int playerIndex)
    {
        switch (playerIndex)
        {
            case 0:
                return first.position;
            case 1:
                return second.position;
            case 2:
                return third.position;
            case 3:
                return fourth.position;
            default:
                return podium.position; // Set default position if there are more than 4 players
        }
    }
}