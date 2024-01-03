using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using TMPro;

public class Podium : MonoBehaviour
{
    public GameObject blocks;
    public GameObject scores;

    [Space]
    public Transform podium;
    public Transform first;
    public Transform second;
    public Transform third;
    public Transform fourth;

    [Space]
    public TMP_Text score1;
    public TMP_Text score2;
    public TMP_Text score3;
    public TMP_Text score4;

    GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    public void MoveToPodium()
    {
        ScoreManager scoreManager = ScoreManager.Instance;
        scoreManager.playerScores = scoreManager.playerScores.OrderByDescending(_player => _player.Value).ToDictionary(_player => _player.Key, _player => _player.Value);

        blocks.SetActive(true);
        scores.SetActive(true);

        for (int i = 0; i < gameManager.players.Count; i++)
        {
            var kvp = scoreManager.playerScores.ElementAt(i);
            var value = scoreManager.playerScores.ElementAt(i).Value;

            gameManager.players[i].transform.SetParent(podium);

            switch (i)
            {
                case 0:
                    kvp.Key.transform.position = first.transform.position;
                    kvp.Key.transform.rotation = first.transform.rotation;
                    kvp.Key.GetComponent<PlayerStateMachine>().playerAnimator.Play("Victory");
                    score1.text = value.ToString();
                    break;
                case 1:
                    kvp.Key.transform.position = second.transform.position;
                    kvp.Key.transform.rotation = second.transform.rotation;
                    kvp.Key.GetComponent<PlayerStateMachine>().playerAnimator.Play("Second");
                    score2.text = value.ToString();
                    break;
                case 2:
                    kvp.Key.transform.position = third.transform.position;
                    kvp.Key.transform.rotation = third.transform.rotation;
                    kvp.Key.GetComponent<PlayerStateMachine>().playerAnimator.Play("Third");
                    score3.text = value.ToString();
                    break;
                case 3:
                    kvp.Key.transform.position = fourth.transform.position;
                    kvp.Key.transform.rotation = fourth.transform.rotation;
                    kvp.Key.GetComponent<PlayerStateMachine>().playerAnimator.Play("Fourth");
                    score4.text = value.ToString();
                    break;
            }
        }
    }
}