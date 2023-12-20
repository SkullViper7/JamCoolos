using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreenButtons : MonoBehaviour
{
    public void Restart()
    {
        GameManager.Instance.ResetManager();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        GameManager.Instance.ClearManager();
        SceneManager.LoadScene("MainMenu");
    }
}
