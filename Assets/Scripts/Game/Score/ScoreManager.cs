using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public Dictionary<string, int> playerScores = new();

    //Singleton
    private static ScoreManager _instance = null;
    public static ScoreManager Instance => _instance;
    //

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
    }

    public void AddScore(string _playerName, int _score)
    {
        playerScores[_playerName] += _score;
    }
}