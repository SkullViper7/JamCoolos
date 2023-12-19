using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class DisplayScore : MonoBehaviour
{
    [SerializeField]
    private List<TMP_Text> ranks = new();
    [SerializeField]
    private List<TMP_Text> names = new();
    [SerializeField]
    private List<TMP_Text> scores = new();

    private void OnEnable()
    {
        DisplayScores();
    }

    public void DisplayScores()
    {
        ScoreManager scoreManager = ScoreManager.Instance;

        scoreManager.playerScores = scoreManager.playerScores.OrderByDescending(_player => _player.Value).ToDictionary(_player => _player.Key, _player => _player.Value);

        for (int i = 0; i < scoreManager.playerScores.Count; i++)
        {
            //Set rank
            ranks[i].text = (i + 1).ToString();

            var kvp = scoreManager.playerScores.ElementAt(i);

            //Set names
            names[i].text = kvp.Key;
            names[i].color = PlayerColor(kvp.Key);

            //Set scores
            scores[i].text = kvp.Value.ToString();
            scores[i].color = PlayerColor(kvp.Key);
        }
    }

    private Color PlayerColor(string _player)
    {
        //Return the color associated to the player given
        switch (_player)
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
