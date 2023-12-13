using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int player1Score;
    public int player2Score;
    public int player3Score;
    public int player4Score;

    [Space]
    public ScoringZone zone;

    public void AddScore(int playerNumber, int score)
    {
        switch (playerNumber)
        {
            case 1:
                player1Score += score;
                break;
            case 2:
                player2Score += score;
                break;
            case 3:
                player3Score += score;
                break;
            case 4:
                player4Score += score;
                break;
        }
    }
}