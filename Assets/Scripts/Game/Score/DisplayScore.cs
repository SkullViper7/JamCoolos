using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class DisplayScore : MonoBehaviour
{
    [SerializeField]
    private List<TMP_Text> _ranks = new();
    [SerializeField]
    private List<TMP_Text> _names = new();
    [SerializeField]
    private List<TMP_Text> _scores = new();

    private void OnEnable()
    {
        DisplayScores();
    }

    public void DisplayScores()
    {
        ScoreManager scoreManager = ScoreManager.Instance;

        scoreManager.playerScores = scoreManager.playerScores.OrderByDescending(_player => _player.Value).ToDictionary(_player => _player.Key, _player => _player.Value);

        //for (int i = 0; i < scoreManager.playerScores.Count; i++)
        //{
        //    //Set rank
        //    _ranks[i].text = (i + 1).ToString();

        //    var kvp = scoreManager.playerScores.ElementAt(i);

        //    //Set names
        //    _names[i].text = kvp.Key;
        //    _names[i].color = PlayerColor(kvp.Key);

        //    //Set scores
        //    _scores[i].text = kvp.Value.ToString();
        //    _scores[i].color = PlayerColor(kvp.Key);
        //}
    }

    private Color PlayerColor(string player)
    {
        //Return the color associated to the player given
        switch (player)
        {
            case "Player1":
                return Color.green;
            case "Player2":
                return Color.red;
            case "Player3":
                return Color.blue;
            case "Player4":
                return Color.yellow;
            default: return Color.white;
        }
    }
}
