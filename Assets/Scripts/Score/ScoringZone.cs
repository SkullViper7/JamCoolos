using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoringZone : MonoBehaviour
{
    bool greenCanScore;
    bool redCanScore;
    bool blueCanScore;
    bool yellowCanScore;

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.name == "GreenZone" && other.gameObject.name == "Player1")
        {
            greenCanScore = true;
        }
        if (gameObject.name == "RedZone" && other.gameObject.name == "Player2")
        {
            redCanScore = true;
        }
        if (gameObject.name == "BlueZone" && other.gameObject.name == "Player3")
        {
            blueCanScore = true;
        }
        if (gameObject.name == "YellowZone" && other.gameObject.name == "Player4")
        {
            yellowCanScore = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        greenCanScore = false;
        redCanScore = false;
        blueCanScore = false;
        yellowCanScore = false;
    }
}