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

        for (int i = 0; i < gameManager.players.Count; i++)
        {
            var kvp = scoreManager.playerScores.ElementAt(i);

            gameManager.players[i].transform.SetParent(podium);

            switch (i)
            {
                case 0:
                    kvp.Key.transform.position = first.transform.position;
                    kvp.Key.transform.rotation = first.transform.rotation;
                    kvp.Key.GetComponent<PlayerStateMachine>().playerAnimator.Play("Victory");
                    break;
                case 1:
                    kvp.Key.transform.position = second.transform.position;
                    kvp.Key.transform.rotation = second.transform.rotation;
                    kvp.Key.GetComponent<PlayerStateMachine>().playerAnimator.Play("Second");
                    break;
                case 2:
                    kvp.Key.transform.position = third.transform.position;
                    kvp.Key.transform.rotation = third.transform.rotation;
                    kvp.Key.GetComponent<PlayerStateMachine>().playerAnimator.Play("Third");
                    break;
                case 3:
                    kvp.Key.transform.position = fourth.transform.position;
                    kvp.Key.transform.rotation = fourth.transform.rotation;
                    kvp.Key.GetComponent<PlayerStateMachine>().playerAnimator.Play("Fourth");
                    break;
            }
        }
    }
}