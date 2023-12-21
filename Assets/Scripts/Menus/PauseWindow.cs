using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Linq;

public class PauseWindow : MonoBehaviour
{
    public void PlayGame()
    {
        //Play the game
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }
}
