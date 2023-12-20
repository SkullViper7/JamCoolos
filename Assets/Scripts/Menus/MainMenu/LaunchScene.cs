using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LaunchScene : MonoBehaviour
{
    public GameObject playerChoice;
    public GameObject mainMenu;

    public Animator camAnim;

    public void LaunchAScene()
    {
        playerChoice.SetActive(true);
        mainMenu.SetActive(false);
        camAnim.Play("SceneSwitch");
    }
}