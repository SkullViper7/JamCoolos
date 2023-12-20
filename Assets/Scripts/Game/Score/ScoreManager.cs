using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

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

    public void AddScore(string playerName, int score)
    {
        //Add score to a player given
        playerScores[playerName] += score;
    }

    public GameObject GetBestPlayer()
    {
        //Return the actual player who has the best score
        string bestPlayer = "";

        for (int i = 0; i < playerScores.Count; i++)
        {
            var kvp = playerScores.ElementAt(i);
            
            if (kvp.Value >= playerScores[kvp.Key])
            {
                bestPlayer = kvp.Key;
            }
        }
        return GameObject.Find(bestPlayer);
    }
}